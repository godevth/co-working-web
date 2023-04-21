using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Queries;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoreCMS.Application.User.Commands
{
    public class EditUserCommandHandler : BaseDbContextWithMediatorHandler<EditUserCommand, CommandResult<string>>
    {
        private UserManager<ApplicationUser> _userManager;

        public EditUserCommandHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager) : base(context, mediator)
        {
            _userManager = userManager;
        }

        public override async Task<CommandResult<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = null;
            cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถแก้ไขข้อมูลสมาชิกได้"
            };
            List<string> errors = request.Validate();
            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try {

                    if(errors.Any())
                    {
                        throw new Exception($"{string.Join(", ", errors.Join(", "))}");
                    }
                    var user = await _userManager.FindByIdAsync(request.UserId);
                    bool isChangeEmail = false;
                    if(user != null)
                    {
                        string email = request.Email.Trim();
                        #region Validate DupEmail
                        if(user.Email.ToLower() != email.ToLower())
                        {
                            var emailExisting = await _userManager.FindByEmailAsync(email);
                            if (emailExisting != null)
                            {
                                throw new Exception("Email นี้มีในระบบแล้ว ไม่สามารถสร้างข้อมูลสมาชิกด้วย Email ซ้ำได้");
                            }
                            isChangeEmail = true;
                        }
                        #endregion
                        
                        #region  Check Edit Email
                        if(user.NormalizedUserName == user.NormalizedEmail)
                        {
                            user.UserName = email;
                            user.NormalizedUserName = email.ToUpper();
                        }
                        #endregion

                        user.Email = email;
                        user.NormalizedEmail = email.ToUpper();
                        user.FirstName = request.FirstName;
                        user.LastName = request.LastName;
                        user.DisplayName = $"{request.FirstName} {request.LastName}".Trim();
                        user.BirthDate = request.BirthDate;
                        user.PhoneNumber = request.PhoneNumber;
                        user.Position = request.Position;
                        user.InActiveStatus = request.InActiveStatus;
                        user.UpdatedDate = DateTime.Now;
                        user.UpdatedUserId = request.UpdatedUserId;

                        if(isChangeEmail)
                        {
                            user.EmailConfirmed = false;
                            user.RandomCode = null;
                            user.ConfirmEmailDate = (DateTime?)null;
                        }

                        IdentityResult updateResult = await _userManager.UpdateAsync(user);
                        if (!updateResult.Succeeded)
                        {
                            throw new Exception($"{string.Join(", ", updateResult.Errors.Select(o => o.Description).ToArray())}");
                        }

                        #region Add/Delete UserRole
                        if(!string.IsNullOrEmpty(request.Role))
                        {
                            bool chk = false;
                            #region  Remove OldRole
                            var userView = await _mediator.Send(
                                new GetUserQuery(){
                                    UserId = user.Id
                            });
                            //change role
                            if(userView.Roles != null && userView.Roles != request.Role)
                            {
                                var delRoleResult = await _userManager.RemoveFromRoleAsync(user, userView.Roles);
                                if (!delRoleResult.Succeeded)
                                {
                                    throw new Exception($"{string.Join(", ", delRoleResult.Errors.Select(o => o.Description).ToArray())}");
                                }
                                chk = true;
                            }
                            #endregion
                            //add role
                            else if(userView.Roles == null){
                                chk = true;
                            }

                            if(chk)
                            {
                                var roleResult = await _userManager.AddToRoleAsync(user, request.Role);
                                if (!roleResult.Succeeded)
                                {
                                    throw new Exception($"{string.Join(", ", roleResult.Errors.Select(o => o.Description).ToArray())}");
                                }
                            }
                        }
                        //delete role
                        else
                        {
                            //default user role (bronze)
                            string userRole = "bronze";

                            var userView = await _mediator.Send(
                                new GetUserQuery(){
                                    UserId = user.Id
                            });
                            
                            if(userView.Roles != null)
                            {
                                if(!userView.Roles.Contains(userRole))
                                {
                                    var delRoleResult = await _userManager.RemoveFromRoleAsync(user, userView.Roles);
                                    if (!delRoleResult.Succeeded)
                                    {
                                        throw new Exception($"{string.Join(", ", delRoleResult.Errors.Select(o => o.Description).ToArray())}");
                                    }

                                    #region AddUserRole
                                    var roleResult = await _userManager.AddToRoleAsync(user, userRole);
                                    if (!roleResult.Succeeded)
                                    {
                                        throw new Exception($"{string.Join(", ", roleResult.Errors.Select(o => o.Description).ToArray())}");
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                #region AddUserRole
                                var roleResult = await _userManager.AddToRoleAsync(user, userRole);
                                if (!roleResult.Succeeded)
                                {
                                    throw new Exception($"{string.Join(", ", roleResult.Errors.Select(o => o.Description).ToArray())}");
                                }
                                #endregion
                            }
                        }
                        #endregion

                         #region SendEmailConfirm
                        if(isChangeEmail)
                        {
                            SendEmailConfirmCommand sendEmailConfirm = new SendEmailConfirmCommand()
                            {
                                User = user,
                                RandomCode = Guid.NewGuid().ToString()
                            };
                            bool sendEmail = await _mediator.Send(sendEmailConfirm);
                            if(!sendEmail)
                                throw new Exception("ไม่สามารถส่งอีเมลยืนยันได้");

                            user.RandomCode = sendEmailConfirm.RandomCode;
                            var updateRandomCode = await _userManager.UpdateAsync(user);
                            if (!updateRandomCode.Succeeded)
                            {
                                throw new Exception($"{string.Join(", ", updateRandomCode.Errors.Select(o => o.Description).ToArray())}");
                            }
                        }
                        #endregion
                        
                        cmdResult.Status = true;
                        cmdResult.Data = user.UserName;
                        cmdResult.Message = "แก้ไขข้อมูลสมาชิกสำเร็จ";
                        if(isChangeEmail)
                            cmdResult.Message += "และกรุณายืนยันตัวตนตามอีเมลที่ได้รับ";
                        trx.Commit();
                    }      
                }
                catch(Exception e)
                {
                    trx.Rollback();
                    cmdResult.Message = e.Message;
                }
            }
            return await Task.FromResult(cmdResult);
        }
    }
}
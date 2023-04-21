using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoreCMS.Application.User.Commands
{
    public class AddUserCommandHandler : BaseDbContextWithMediatorHandler<AddUserCommand, CommandResult<string>>
    {
        private UserManager<ApplicationUser> _userManager;
        public AddUserCommandHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager) : base(context, mediator)
        {
            _userManager = userManager;
        }

        public override async Task<CommandResult<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = null;

            cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถสร้างข้อมูลสมาชิกได้"
            };
            
            List<string> errors = request.Validate();

            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try {
                    if(errors.Any())
                    {
                        throw new Exception($"{string.Join(", ", errors.Join(", "))}");
                    }
                    
                    #region Validate DupEmail
                    var emailExisting = await _userManager.FindByEmailAsync(request.Email.Trim());
                    if (emailExisting != null)
                    {
                        throw new Exception("Email นี้มีในระบบแล้ว ไม่สามารถสร้างข้อมูลสมาชิกด้วย Email ซ้ำได้");
                    }
                    #endregion
                    
                    var user = new ApplicationUser
                    {
                        UserName = request.Email.Trim(),
                        Email = request.Email.Trim(),
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        DisplayName = $"{request.FirstName} {request.LastName}".Trim(),
                        PhoneNumber = request.PhoneNumber,
                        Position = request.Position,
                        BirthDate = request.BirthDate,
                        InActiveStatus = request.InActiveStatus,
                        CreateUserDateTime = DateTime.Now,
                        EmailConfirmed = false,
                        TwoFactorEnabled = true,
                        NormalizedEmail = request.Email.Trim().ToUpper(),
                        NormalizedUserName = request.Email.Trim().ToUpper(),
                        CreatedUserId = request.CreateUserId,
                        CreatedDate = DateTime.Now,
                    };
                    
                    IdentityResult createResult = await _userManager.CreateAsync(user, request.Password);
                
                    if (!createResult.Succeeded)
                    {
                        throw new Exception($"{string.Join(", ", createResult.Errors.Select(o => o.Description).ToArray())}");
                    }
                    
                    #region AddUserRole
                    //default user role (bronze)
                    string userRole = "bronze";
                    if(!string.IsNullOrEmpty(request.Role))
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, request.Role);
                        if (!roleResult.Succeeded)
                        {
                            throw new Exception($"{string.Join(", ", roleResult.Errors.Select(o => o.Description).ToArray())}");
                        }
                    }
                    else
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, userRole);
                        if (!roleResult.Succeeded)
                        {
                            throw new Exception($"{string.Join(", ", roleResult.Errors.Select(o => o.Description).ToArray())}");
                        }
                    }
                    #endregion

                    cmdResult.Status = true;

                    #region SendEmailConfirm
                    if(cmdResult.Status)
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

                    cmdResult.Data = user.UserName;
                    cmdResult.Message = "สร้างข้อมูลสมาชิกสำเร็จและกรุณายืนยันตัวตนตามอีเมลที่ได้รับ";
                    trx.Commit();
                }
                catch (Exception e) {
                    trx.Rollback();
                    cmdResult.Message = e.Message;
                }
            }
            return await Task.FromResult(cmdResult);
        }
    }
}
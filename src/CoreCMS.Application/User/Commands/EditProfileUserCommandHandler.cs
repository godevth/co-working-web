using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Picture.Commands;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Data;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;

namespace CoreCMS.Application.User.Commands
{
    public class EditProfileUserCommandHandler : BaseDbContextWithMediatorHandler<EditProfileUserCommand, CommandResult<string>>
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly OpenIDConfig _config;

        public EditProfileUserCommandHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager, IOptions<OpenIDConfig> config) : base(context, mediator)
        {
            _userManager = userManager;
            _config = config.Value;
        }

        public override async Task<CommandResult<string>> Handle(EditProfileUserCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = null;
            List<string> errors = request.Validate();

            cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถแก้ไขข้อมูลได้"
            };

            using (var trx = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (errors.Any())
                    {
                        cmdResult = new CommandResult<string>()
                        {
                            Errors = new Dictionary<string, string[]>
                            {
                                { "MODEL_VALIDATION", errors.ToArray() }
                            }
                        };
                        return await Task.FromResult(cmdResult);
                    }


                    var user = await _userManager.FindByIdAsync(request.UserId);
                    bool isEditEmail = false;
                    if (user != null)
                    {
                        user.FirstName = request.FirstName;
                        user.LastName = request.LastName;
                        user.DisplayName = $"{request.FirstName} {request.LastName}".Trim();
                        user.Address = request.Address;
                        user.TumbolId = request.TumbolId;
                        user.AmphurId = request.AmphurId;
                        user.ProvinceId = request.ProvinceId;
                        user.PostCode = request.PostCode;
                        user.PhoneNumber = request.PhoneNumber;
                        user.PhoneCountryCode = request.PhoneCountryCode;
                        user.Gender = request.Gender;
                        user.BirthDate = request.BirthDateTime;
                        user.Companyname = request.Companyname;
                        user.UpdatedDate = DateTime.Now;
                        user.UpdatedUserId = request.UpdatedUserId;

                        // string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        // var result = await _userManager.ResetPasswordAsync(user, token.Trim(), request.Password.Trim());
                        // if(!result.Succeeded)
                        // {
                        //     throw new Exception($"{string.Join(", ", result.Errors.Select(o => o.Description).ToArray())}");
                        // }

                        if (user.Email.Trim().ToLower() == "temporary@cowapp.com" && !string.IsNullOrEmpty(request.Email))
                        {
                            #region Validate DupEmail
                            var emailExisting = _context.Users.Where(o => o.NormalizedEmail == request.Email.Trim().ToUpper()).Any();
                            if (emailExisting)
                            {
                                throw new Exception("Email นี้มีในระบบแล้ว ไม่สามารถใช้ Email ที่ซ้ำกันได้");
                            }
                            #endregion

                            user.Email = request.Email;
                            user.NormalizedEmail = request.Email.ToUpper();
                            user.EmailConfirmed = false;
                            user.ConfirmEmailDate = null;
                            user.RandomCode = null;
                            isEditEmail = true;
                        }

                        IdentityResult updateResult = await _userManager.UpdateAsync(user);

                        if (!updateResult.Succeeded)
                        {
                            throw new Exception($"{string.Join(", ", updateResult.Errors.Select(o => o.Description).ToArray())}");
                        }

                        #region SendEmailConfirm
                        if(isEditEmail)
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

                        if(isEditEmail)
                            cmdResult.Message += "และกรุณายืนยันตัวตนตามอีเมลที่ได้รับ";

                        trx.Commit();
                    }

                }
                catch (Exception e)
                {
                    trx.Rollback();
                    errors.Add(e.Message);
                    cmdResult = new CommandResult<string>()
                    {
                        Errors = new Dictionary<string, string[]>
                        {
                            { "CREATE_RESULT", errors.ToArray() }
                        }
                    };
                }
            }

            return await Task.FromResult(cmdResult);
        }
    }
}
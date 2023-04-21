using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Notification.Commands;
using CoreCMS.Application.Notification.Models;
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
    public class RegisterCommandHandler : BaseDbContextWithMediatorHandler<RegisterCommand, CommandResult<string>>
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly OpenIDConfig _config;

        public RegisterCommandHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager, IOptions<OpenIDConfig> config) : base(context, mediator)
        {
            _userManager = userManager;
            _config = config.Value;
        }

        public override async Task<CommandResult<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = null;
            //Validate Model
            List<string> errors = request.Validate();

            cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถสร้างข้อมูลสมาชิกได้"
            };
            
            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try {

                    #region Validate DupEmail
                    var emailExisting = await _userManager.FindByEmailAsync(request.Email.Trim());
                    if (emailExisting != null)
                    {
                        errors.Add("Email นี้มีในระบบแล้ว ไม่สามารถสร้างข้อมูลสมาชิกด้วย Email ซ้ำได้");
                    }
                    #endregion

                    if(errors.Any())
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
                    
                    var user = new ApplicationUser
                    {
                        UserName = request.Email.Trim(),
                        Email = request.Email.Trim(),
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        DisplayName = $"{request.FirstName} {request.LastName}".Trim(),
                        Address = request.Address,
                        TumbolId = request.TumbolId,
                        AmphurId = request.AmphurId,
                        ProvinceId = request.ProvinceId,
                        PostCode = request.PostCode,
                        PhoneNumber = request.PhoneNumber,
                        PhoneCountryCode = request.PhoneCountryCode,
                        Gender = request.Gender,
                        BirthDate = request.BirthDateTime,
                        CreateUserDateTime = DateTime.Now,
                        EmailConfirmed = request.IsExternalProvider,
                        TwoFactorEnabled = true,
                        NormalizedEmail = request.Email.Trim().ToUpper(),
                        NormalizedUserName = request.Email.Trim().ToUpper(),
                        //
                        OpenID = request.IsExternalProvider && request.LoginProvider == _config.Authority
                    };

                    IdentityResult createResult = await _userManager.CreateAsync(user, request.Password);
                
                    if (!createResult.Succeeded)
                    {
                        throw new Exception($"{string.Join(", ", createResult.Errors.Select(o => o.Description).ToArray())}");
                    }
                    
                    #region AddUserLogin
                    if(request.IsExternalProvider)
                    {
                        var loginResult = await _userManager.AddLoginAsync(user, new UserLoginInfo(request.LoginProvider, request.ProviderKey, request.ProviderDisplayName));

                        if (!loginResult.Succeeded)
                            throw new Exception($"{string.Join(", ", loginResult.Errors.Select(o => o.Description).ToArray())}");
                        
                    }
                    #endregion
                    
                    #region AddUserRole
                    //default user role (bronze)
                    string userRole = "bronze";
                    var roleResult = await _userManager.AddToRoleAsync(user, userRole);
                    if (!roleResult.Succeeded)
                    {
                        throw new Exception($"{string.Join(", ", roleResult.Errors.Select(o => o.Description).ToArray())}");
                    }
                    #endregion
                    
                    cmdResult.Status = true;
                    
                    #region SendEmailConfirm
                    if(cmdResult.Status && !request.IsExternalProvider)
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

                    cmdResult.Data = user.Id;
                    cmdResult.Message = "สร้างข้อมูลสมาชิกสำเร็จ";
                    if(!request.IsExternalProvider)
                        cmdResult.Message += "และกรุณายืนยันตัวตนตามอีเมลที่ได้รับ";
                    trx.Commit ();
                }
                catch (Exception e) {
                    trx.Rollback ();
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
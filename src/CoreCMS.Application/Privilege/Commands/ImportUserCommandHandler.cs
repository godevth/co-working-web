using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Commands;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CoreCMS.Application.Privilege.Commands
{
    public class ImportUserCommandHandler : BaseDbContextWithMediatorHandler<ImportUserCommand, CommandResult<string>>
    {
        private UserManager<ApplicationUser> _userManager;
        public ImportUserCommandHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager) : base(context, mediator)
        {
            _userManager = userManager;
        }

        public override async Task<CommandResult<string>> Handle(ImportUserCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = null;

            cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถสร้างข้อมูลสมาชิกได้"
            };

            using (var trx = await _context.Database.BeginTransactionAsync ()) 
            {
                try
                {
                    foreach(var item in request.ListUser)
                    {
                        if(item.Email == "example@mail.com" && item.Firstname == "Example" && item.Lastname =="Example")
                        {
                            throw new Exception("กรุณาลบข้อมูลตัวอย่าง");
                        }
                        #region Validate DupEmail
                        var emailExisting = await _userManager.FindByEmailAsync(item.Email.Trim());
                        if (emailExisting != null)
                        {
                            throw new Exception("มี Email ซ้ำกันในระบบ ไม่สามารถสร้างข้อมูลสมาชิกด้วย Email ซ้ำได้");
                        }
                        #endregion

                        var user = new ApplicationUser
                        {
                            UserName = item.Email.Trim(),
                            Email = item.Email.Trim(),
                            FirstName = item.Firstname,
                            LastName = item.Lastname,
                            DisplayName = $"{item.Firstname} {item.Lastname}".Trim(),
                            PhoneNumber = item.PhoneNo,
                            CreateUserDateTime = DateTime.Now,
                            EmailConfirmed = false,
                            TwoFactorEnabled = true,
                            NormalizedEmail = item.Email.Trim().ToUpper(),
                            NormalizedUserName = item.Email.Trim().ToUpper(),
                            CreatedUserId = request.CreateUserId,
                            CreatedDate = DateTime.Now,
                            Gender = item.Gender
                        };
                        IdentityResult createResult = await _userManager.CreateAsync(user, item.Password);
                
                        if (!createResult.Succeeded)
                        {
                            throw new Exception($"{string.Join(", ", createResult.Errors.Select(o => o.Description).ToArray())}");
                        }
                        var res = await _context.SaveChangesAsync();
                        #region SendEmailConfirm
                        if(res == 0)
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
                    }
                    cmdResult.Status = true;
                    cmdResult.Message = "สร้างข้อมูลสมาชิกสำเร็จและกรุณายืนยันตัวตนตามอีเมลที่ได้รับ";
                    trx.Commit();

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
using System;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using Microsoft.AspNetCore.Identity;

namespace CoreCMS.Application.User.Commands
{
    public class GenResetPasswordTokenCommandHandler : BaseDbContextHandler<GenResetPasswordTokenCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GenResetPasswordTokenCommandHandler(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public override async Task<string> Handle(GenResetPasswordTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
                throw new Exception(string.Format("ไม่พบผู้ใช้งาน {0}", request.UserId));

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }
    }
}
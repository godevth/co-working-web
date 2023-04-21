using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.User.Models;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CoreCMS.Application.User.Queries
{
    public class GetUserQueryHandler : BaseDbContextWithMediatorHandler<GetUserQuery, UserView>
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        public GetUserQueryHandler(ApplicationDbContext context, IMediator mediator, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) : base(context, mediator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public override async Task<UserView> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            UserView model = null;
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user != null)
            {
                model = new UserView()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    BirthDate= user.BirthDate,
                    Position = user.Position,
                    UserId = user.Id,
                    InActiveStatus = user.InActiveStatus,
                    IsDeleted = user.IsDeleted,
                    OpenID = user.OpenID
                    //UserTypeId = user.UserTypeId.HasValue ? user.UserTypeId.ToString() : null,
                };

                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Count > 0)
                {
                    var role = await _roleManager.FindByNameAsync(roles[0]);
                    model.Roles = role.Name;
                    model.UserTypeId = role.UserTypeId.HasValue ? role.UserTypeId.Value.ToString() : null;
                }
            }

            return await Task.FromResult(model);
        }
    }
}
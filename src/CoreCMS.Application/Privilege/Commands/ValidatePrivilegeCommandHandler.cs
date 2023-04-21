using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.User.Queries;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Privilege.Commands
{
    public class ValidatePrivilegeCommandHandler : BaseDbContextWithMediatorHandler<ValidatePrivilegeCommand, bool>
    {
        public ValidatePrivilegeCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<bool> Handle(ValidatePrivilegeCommand request, CancellationToken cancellationToken)
        {
            bool chk = false;
            GetUserQuery userQuery = new GetUserQuery()
            {
                UserId = request.CurrentUserId
            };
            var user = await _mediator.Send(userQuery);
            if(user == null)
                return await Task.FromResult(chk);

            var privileges = _context.Privilege.Where(o => !o.IsDeleted && !o.InActiveStatus && o.PlaceId == request.PlaceId).ToList();
            if(privileges.Any())
            {
                //PRIVILEGE_TYPE_PERSON
                chk = privileges.Where(o => o.PrivilegeTypeCode == "PRIVILEGE_TYPE_PERSON" && o.UserId == user.UserId).Any();

                //PRIVILEGE_TYPE_DOMAIN
                if(!chk)
                {
                    chk = privileges.Where(o => o.PrivilegeTypeCode == "PRIVILEGE_TYPE_DOMAIN" && user.Email.Contains(o.Domain)).Any();
                }
            }

            return await Task.FromResult(chk);
        }
    }
}
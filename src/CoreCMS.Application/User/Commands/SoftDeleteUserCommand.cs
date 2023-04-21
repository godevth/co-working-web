using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.User.Commands
{
    public class SoftDeleteUserCommand : IRequest<CommandResult<bool>>
    {
        public string UserId { get; set; }
        public string DeleteUserId { get; set; }
    }
}
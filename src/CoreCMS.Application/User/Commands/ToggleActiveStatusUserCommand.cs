using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.User.Commands
{
    public class ToggleActiveStatusUserCommand : IRequest<CommandResult<bool>>
    {
        public string UserId { get; set; }
        public string UpdatedUserId { get; set; }
    }
}
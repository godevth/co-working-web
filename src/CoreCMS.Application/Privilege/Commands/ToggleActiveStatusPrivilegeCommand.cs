using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Privilege.Commands
{
    public class ToggleActiveStatusPrivilegeCommand : IRequest<CommandResult<bool>>
    {
        public int PrivilegeId { get; set; }
        public string UpdatedUserId { get; set; }
    }
}
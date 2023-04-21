using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Privilege.Commands
{
    public class SoftDeletePrivilegeCommand : IRequest<CommandResult<bool>>
    {
        public int PrivilegeId { get; set; }
        public string DeleteUserId { get; set; }
    }
}
using CoreCMS.Application.Privilege.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Privilege.Commands
{
    public class InviteUserCommand : PrivilegeForm, IRequest<CommandResult<string>>
    {
        public string CreateUserId { get; set; }
    }
}
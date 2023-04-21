using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.UserType.Commands
{
    public class ToggleUserTypeCommand : IRequest<CommandResult<bool>>
    {
        public int UserTypeId { get; set; }
    }
}
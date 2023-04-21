using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Role.Commands
{
    public class EditRoleCommand:IRequest<CommandResult<bool>>
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? UserTypeId { get; set; }
        public string UserTypeName { get; set; }
    }
}
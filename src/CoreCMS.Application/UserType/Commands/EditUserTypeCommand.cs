using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.UserType.Commands
{
    public class EditUserTypeCommand:IRequest<CommandResult<bool>>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool InActiveStatus { get; set; }
    }
}
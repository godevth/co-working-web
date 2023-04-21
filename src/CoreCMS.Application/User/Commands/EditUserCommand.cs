using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using MediatR;

namespace CoreCMS.Application.User.Commands
{
    public class EditUserCommand : UserForm, IRequest<CommandResult<string>>
    {
        public string UserId { get; set; }
        public string UpdatedUserId { get; set; }
    }
}
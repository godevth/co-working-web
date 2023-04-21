using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using MediatR;

namespace CoreCMS.Application.User.Commands
{
    public class AddUserCommand : UserForm, IRequest<CommandResult<string>>
    {
        public string CreateUserId { get; set; }
    }
}
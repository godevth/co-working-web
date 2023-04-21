using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.User.Commands
{
    public class ForgotPasswordCommand : IRequest<CommandResult<string>>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
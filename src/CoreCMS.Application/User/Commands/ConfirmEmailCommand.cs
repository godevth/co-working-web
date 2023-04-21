using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.User.Commands
{
    public class ConfirmEmailCommand : IRequest<CommandResult<string>>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RandomCode { get; set; }
    }
}
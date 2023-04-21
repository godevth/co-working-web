using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.User.Commands
{
    public class ResetPasswordCommand : IRequest<CommandResult<string>>
    {
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public string Token { get; set; }

        [DataType(DataType.Password)]
        [StringLength(20)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [StringLength(20)]
        [Compare("Password")]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
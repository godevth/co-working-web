using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.User.Commands
{
    public class ChangePasswordCommand : IRequest<CommandResult<string>>
    {
        public string UserId { get; set; }

        [DataType(DataType.Password)]
        [StringLength(20)]
        [Required]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [StringLength(20)]
        [Required]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [StringLength(20)]
        [Compare("NewPassword")]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
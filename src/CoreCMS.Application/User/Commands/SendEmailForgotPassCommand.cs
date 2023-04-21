using System.ComponentModel.DataAnnotations;
using CoreCMS.Domain;
using MediatR;

namespace CoreCMS.Application.User.Commands
{
    public class SendEmailForgotPassCommand : IRequest<bool>
    {
        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public string CallbackUrl { get; set; }
    }
}
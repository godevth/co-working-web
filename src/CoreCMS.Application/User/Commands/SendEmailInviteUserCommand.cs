using System.ComponentModel.DataAnnotations;
using CoreCMS.Domain;
using MediatR;

namespace CoreCMS.Application.User.Commands
{
    public class SendEmailInviteUserCommand : IRequest<bool>
    {
        [Required]
        public ApplicationUser User { get; set; }

        public string PlaceName { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using CoreCMS.Domain;
using MediatR;

namespace CoreCMS.Application.User.Commands
{
    public class SendEmailConfirmedCommand : IRequest<bool>
    {
        [Required]
        public ApplicationUser User { get; set; }
    }
}
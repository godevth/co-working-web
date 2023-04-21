using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Privilege.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Privilege.Commands
{
    public class AddUserCommand : IRequest<CommandResult<string>>
    {
        public string PlaceId { get; set;}
        public string CreateUserId { get; set; }

        [Required]
        public string FirstName { get; set;}
        [Required]
        public string LastName { get; set;}
        [Required]
        public string Email { get; set;}
        [Required]
        public string Password { get; set;}
        [Required]
        public string ConfirmPassword { get; set;}
        [Required]
        public string Gender { get; set;}
        [Required]
        public string PhoneNumber { get; set;}
    }
}
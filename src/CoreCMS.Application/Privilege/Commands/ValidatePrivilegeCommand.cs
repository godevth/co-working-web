using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace CoreCMS.Application.Privilege.Commands
{
    public class ValidatePrivilegeCommand : IRequest<bool>
    {
        [Required]
        public Guid PlaceId { get; set; }

        [Required]
        public string CurrentUserId { get; set; }
    }
}
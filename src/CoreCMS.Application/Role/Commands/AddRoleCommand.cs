using System.Collections.Generic;
using CoreCMS.Application.Facility.Models;
using CoreCMS.Application.Room.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Role.Commands
{
    public class AddRoleCommand : IRequest<CommandResult<bool>>
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public int UserTypeId { get; set; }
        public bool InActiveStatus { get; set; }
    }
}
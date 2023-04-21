using System.Collections.Generic;
using CoreCMS.Application.Facility.Models;
using CoreCMS.Application.Room.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Facility.Commands
{
    public class AddFacilityCommand:IRequest<CommandResult<bool>>
    {

        public string Name_TH { get; set; }
        public string Name_EN { get; set; }
        public string FacilityTypeId { get; set; }
        public bool InActiveStatus { get; set; }
        public string CreateUserId { get; set; }
        public List<Pictures> Pictures { get; set; }
    }
}
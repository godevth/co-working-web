using CoreCMS.Application.Facility.Models;
using CoreCMS.Application.Room.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;
using System;
using System.Collections.Generic;

namespace CoreCMS.Application.Facility.Commands
{
    public class EditFacilityCommand:IRequest<CommandResult<bool>>
    {

        public Guid Id { get; set; }
        public string Name_TH { get; set; }
        public string Name_EN { get; set; }
        public string FacilityTypeId { get; set; }
        public bool InActiveStatus { get; set; }
        public string UpdateUserId { get; set; }
         public List<Pictures> Pictures { get; set; }
    }
}
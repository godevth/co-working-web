using CoreCMS.Application.FacilityType.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.FacilityType.Commands
{
    public class EditFacilityTypeCommand:IRequest<CommandResult<bool>>
    {

        public int Id { get; set; }
        public string Name_TH { get; set; }
        public string Name_EN { get; set; }

        public bool InActiveStatus { get; set; }
        public string UpdateUserId { get; set; }
    }
}
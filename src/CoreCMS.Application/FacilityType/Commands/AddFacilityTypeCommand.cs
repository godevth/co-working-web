using CoreCMS.Application.FacilityType.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.FacilityType.Commands
{
    public class AddFacilityTypeCommand:IRequest<CommandResult<bool>>
    {

        public string Name_TH { get; set; }
        public string Name_EN { get; set; }

        public bool InActiveStatus { get; set; }
        public string CreateUserId { get; set; }
    }
}
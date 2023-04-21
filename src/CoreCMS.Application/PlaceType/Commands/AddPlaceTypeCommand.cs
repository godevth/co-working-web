using CoreCMS.Application.PlaceType.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.PlaceType.Commands
{
    public class AddPlaceTypeCommand:IRequest<CommandResult<bool>>
    {

        public string Name { get; set; }
        public string NameEN { get; set; }
        public bool InActiveStatus { get; set; }
        public string CreateUserId { get; set; }
    }
}
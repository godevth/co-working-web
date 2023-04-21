using CoreCMS.Application.PlaceType.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.PlaceType.Commands
{
    public class EditPlaceTypeCommand:IRequest<CommandResult<bool>>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }

        public bool InActiveStatus { get; set; }
        public string UpdateUserId { get; set; }
    }
}
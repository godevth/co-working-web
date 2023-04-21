using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.PlaceType.Commands
{
    public class SoftDeletePlaceTypeCommand : IRequest<CommandResult<bool>>
    {
        public int Id { get; set; }
        public string DeleteUserId { get; set; }
    }
}
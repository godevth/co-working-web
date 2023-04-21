using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Place.Commands
{
    public class ToggleWishlistCommand : IRequest<CommandResult<bool>>
    {
        public string PlaceId { get; set; }
        public string UserId { get; set; }
    }
}
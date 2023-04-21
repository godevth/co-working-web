using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.PlaceTheme.Commands
{
    public class SoftDeletePlaceThemeCommand : IRequest<CommandResult<bool>>
    {
        public int ThemeId { get; set; }
        public string DeleteUserId { get; set; }
    }
}
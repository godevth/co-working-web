using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.PlaceTheme.Commands
{
    public class ToggleActiveStatusPlaceThemeCommand : IRequest<CommandResult<bool>>
    {
        public int ThemeId { get; set; }
        public string UpdatedUserId { get; set; }
    }
}
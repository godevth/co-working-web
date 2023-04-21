using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.PlaceTheme.Commands
{
    public class ChangeThemeCommand : IRequest<CommandResult<bool>>
    {
        public int ThemeId { get; set; }
        public string CurrentUserId { get; set; }
    }
}
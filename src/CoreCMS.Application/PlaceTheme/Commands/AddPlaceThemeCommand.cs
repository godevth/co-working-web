using CoreCMS.Application.PlaceTheme.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.PlaceTheme.Commands
{
    public class AddPlaceThemeCommand : PlaceThemeForm, IRequest<CommandResult<string>>
    {
        public string CreateUserId { get; set; }
    }
}
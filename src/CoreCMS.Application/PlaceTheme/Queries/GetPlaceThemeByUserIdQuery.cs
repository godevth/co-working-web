using CoreCMS.Application.PlaceTheme.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.PlaceTheme.Queries
{
    public class GetPlaceThemeByUserIdQuery : IRequest<SearchMobileResult<PlaceThemeView>>
    {
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
        public string Language { get; set; }
        public string UserId { get; set; }
    }
}
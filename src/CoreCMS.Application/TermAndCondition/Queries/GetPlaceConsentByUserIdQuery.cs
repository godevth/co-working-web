using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.TermAndCondition.Models;
using MediatR;

namespace CoreCMS.Application.TermAndCondition.Queries
{
    public class GetPlaceConsentByUserIdQuery : IRequest<SearchMobileResult<PlaceConsentView>>
    {
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
        public string Language { get; set; }
        public string UserId { get; set; }
    }
}
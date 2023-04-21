using CoreCMS.Application.UserType.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.UserType.Queries
{
    public class SearchUserTypeQuery : IRequest<SearchResult<UserTypeView>>
    {
        public UserTypeDataTable DataTable { get; set; }
    }
}

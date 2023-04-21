using CoreCMS.Application.Role.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Role.Queries
{
    public class SearchRoleQuery : IRequest<SearchResult<RoleView>>
    {
        public RoleDataTable DataTable { get; set; }
    }
}

using CoreCMS.Application.Privilege.Models;
using MediatR;

namespace CoreCMS.Application.Privilege.Queries
{
    public class GetPrivilegeQuery : IRequest<PrivilegeView> 
    {
        public int PrivilegeId { get; set; }
    }
}
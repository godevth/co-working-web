using CoreCMS.Application.Privilege.Models;
using MediatR;

namespace CoreCMS.Application.Privilege.Queries
{
    public class GetPlaceByUserIdQuery : IRequest<PrivilegePlace> 
    {
        public string UserId { get; set; }
        public bool IsCheckConsent { get; set; } = true;
    }
}
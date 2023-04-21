using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using MediatR;

namespace CoreCMS.Application.User.Queries
{
    public class GetUserProfileQuery : IRequest<UserProfile> 
    {
        public string UserId { get; set; }
    }
}
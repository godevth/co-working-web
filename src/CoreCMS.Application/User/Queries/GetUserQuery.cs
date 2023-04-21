using CoreCMS.Application.User.Models;
using MediatR;

namespace CoreCMS.Application.User.Queries
{
    public class GetUserQuery : IRequest<UserView> 
    {
        public string UserId { get; set; }
    }
}
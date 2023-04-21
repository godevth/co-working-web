using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Role.Queries
{
    public class GetRoleOptionsQuery : IRequest<OptionViewModel[]>
    {
        public string Query { get; set; }
        public string UserTypeId { get; set; }
    }
}
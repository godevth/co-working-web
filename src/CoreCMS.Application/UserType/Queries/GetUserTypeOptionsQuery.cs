using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.UserType.Queries
{
    public class GetUserTypeOptionsQuery : IRequest<OptionViewModel[]>
    {
        public string Query { get; set; }
    }
}
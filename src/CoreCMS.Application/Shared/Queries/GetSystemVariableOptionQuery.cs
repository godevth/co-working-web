using CoreCMS.Application.Shared.Model;
using MediatR;
using SBP.Application.Shared.Models;

namespace CoreCMS.Application.Shared.Queries
{
    public class GetSystemVariableOptionQuery : IRequest<OptionViewModel[]>
    {
        public string Search { get; set; }

        public bool IsData { get; set; }
    }
}
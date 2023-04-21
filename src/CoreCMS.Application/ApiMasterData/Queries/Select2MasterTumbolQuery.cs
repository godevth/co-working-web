using CoreCMS.Application.Shared.Model;
using MediatR;
using SBP.Common.Data.Select2;

namespace SBP.Application.ApiMasterData.Queries
{
    public class Select2MasterTumbolQuery : IRequest<OptionViewModel[]>
    {
        public string TumbolId { get; set; }
        public string AmpherId { get; set; }
        public string Search { get; set; }
    }
}
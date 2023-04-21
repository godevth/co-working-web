using CoreCMS.Application.Shared.Model;
using MediatR;
using SBP.Common.Data.Select2;

namespace SBP.Application.ApiMasterData.Queries
{
    public class Select2MasterAmpherQuery : IRequest<OptionViewModel[]>
    {
        public string ProvinceId { get; set; }
        public string AmpherId { get; set; }
        public string Search { get; set; }
        // public int Page { get; set; }
    }
}
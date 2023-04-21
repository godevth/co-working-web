using CoreCMS.Application.Shared.Model;
using MediatR;
using SBP.Common.Data.Select2;

namespace SBP.Application.ApiMasterData.Queries
{
    public class Select2MasterProvinceQuery : IRequest<OptionViewModel[]>
    {
        public string province_id { get; set; }
        public string Search { get; set; }
    }
}
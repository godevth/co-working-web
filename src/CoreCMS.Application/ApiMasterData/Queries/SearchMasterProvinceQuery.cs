using System.Collections.Generic;
using CoreCMS.Application.ApiMasterData.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.ApiMasterData.Queries
{
    public class SearchMasterProvinceQuery : IRequest<CommandResult<List<MasterProvince>>>
    {
        public string ProvinceId { get; set; }
    }
}
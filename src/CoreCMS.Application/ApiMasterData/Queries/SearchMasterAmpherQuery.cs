using System.Collections.Generic;
using CoreCMS.Application.ApiMasterData.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.ApiMasterData.Queries
{
    public class SearchMasterAmpherQuery : IRequest<CommandResult<List<MasterAmpher>>>
    {
        public string ProvinceId { get; set; }
        public string AmpherId { get; set; }
    }
}
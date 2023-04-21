using System.Collections.Generic;
using CoreCMS.Application.ApiMasterData.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.ApiMasterData.Queries
{
    public class SearchMasterTumbolQuery : IRequest<CommandResult<List<MasterTumbol>>>
    {
        public string AmpherId { get; set; }

        public string TumbolId { get; set; }
    }
}
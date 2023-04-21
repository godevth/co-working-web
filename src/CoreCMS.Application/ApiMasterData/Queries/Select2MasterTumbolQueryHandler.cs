using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.ApiMasterData.Queries;
using CoreCMS.Application.Shared.Model;
using MediatR;
using SBP.Common.Data.Select2;
using Microsoft.EntityFrameworkCore;

namespace SBP.Application.ApiMasterData.Queries
{
    public class Select2MasterTumbolQueryHandler : IRequestHandler<Select2MasterTumbolQuery, OptionViewModel[]>
    {
        private readonly IMediator _mediator;
        public Select2MasterTumbolQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OptionViewModel[]> Handle(Select2MasterTumbolQuery request, CancellationToken cancellationToken)
        {
            // Set default
            //request.Page = request.Page > 0 ? request.Page : 1;
            string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToUpper() : string.Empty;
            //int page = request.Page - 1;

            var query = await _mediator.Send(new SearchMasterTumbolQuery() { AmpherId = request.AmpherId });

            var result = query.Data.Select(x => new OptionViewModel() { Value = x.TumbolId, Text = x.TumbolTH ,Data = x.PostCode}).ToArray();

            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(x => x.Text.Contains(search)).ToArray();
            }

            return await Task.FromResult<OptionViewModel[]>(result);
        }
    }
}
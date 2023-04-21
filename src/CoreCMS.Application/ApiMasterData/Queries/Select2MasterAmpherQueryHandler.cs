using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.ApiMasterData.Queries;
using CoreCMS.Application.Shared.Model;
using MediatR;
using SBP.Common.Data.Select2;

namespace SBP.Application.ApiMasterData.Queries
{
    public class Select2MasterAmpherQueryHandler : IRequestHandler<Select2MasterAmpherQuery, OptionViewModel[]>
    {
        private readonly IMediator _mediator;
        public Select2MasterAmpherQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OptionViewModel[]> Handle(Select2MasterAmpherQuery request, CancellationToken cancellationToken)
        {
            // Set default
            //request.Page = request.Page > 0 ? request.Page : 1;
            string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToUpper() : string.Empty;
            //int page = request.Page - 1;

            var query = await _mediator.Send(new SearchMasterAmpherQuery() { ProvinceId = request.ProvinceId });

            var result = query.Data.Select(x => new OptionViewModel() { Value = x.AmpherId, Text = x.AmpherTH }).ToArray();

            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(x => x.Text.Contains(search)).ToArray();
            }

            return await Task.FromResult<OptionViewModel[]>(result);
        }
    }
}
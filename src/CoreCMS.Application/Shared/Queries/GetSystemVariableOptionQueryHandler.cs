using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoreCMS.Application.Shared.Queries
{
    public class GetSystemVariableOptionQueryHandler : BaseDbContextHandler<GetSystemVariableOptionQuery, OptionViewModel[]>
    {
        public GetSystemVariableOptionQueryHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<OptionViewModel[]> Handle(GetSystemVariableOptionQuery request, CancellationToken cancellationToken)
        {
            OptionViewModel[] result = null;
            string search = !string.IsNullOrEmpty(request.Search) ? request.Search.ToUpper() : string.Empty;

            var query = _context.SystemVariables
                .Where(o => o.SystemVariableCategoryCode == search)
                .OrderBy(o => o.Ordering).ThenBy(o => o.SystemVariableName)
                .Select(o => new OptionViewModel()
                {
                    Value = o.SystemVariableCode,
                    Text = o.SystemVariableName,
                    Data = request.IsData == true ? new {
                        Order = o.Ordering,
                        Code = o.SystemVariableCategoryCode,
                        CodeName = o.SystemVariableCategory.SystemVariableCategoryName
                    } : null
                });

            result = await query.ToArrayAsync();
        
            return await Task.FromResult<OptionViewModel[]>(result);
        }
    }
}
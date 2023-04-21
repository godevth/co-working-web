using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.ImplementationDate.Queries;
using CoreCMS.Application.Shared;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Place.Commands
{
    public class IsOpeningCommandHandler : BaseDbContextWithMediatorHandler<IsOpeningCommand, bool>
    {
        public IsOpeningCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<bool> Handle(IsOpeningCommand request, CancellationToken cancellationToken)
        {
            //Query Open Date of Place
            SearchImplementationDateQuery dateQuery = new SearchImplementationDateQuery()
            {
                PlaceId = request.PlaceId,
                StartDays = new List<string>(){ request.StartDay }
            };
            var dates = await _mediator.Send(dateQuery);

            if(request.StartTimeInt > request.EndTimeInt)
                return await Task.FromResult(false);

            bool chk = true;
            foreach(var date in dates)
            {
                if(request.StartTimeInt < date.StartTimeInt)
                {
                    chk = false;
                    break;
                }

                if(request.EndTimeInt > date.EndTimeInt)
                {
                    chk = false;
                    break;
                }
            }

            return await Task.FromResult(chk);
        }
    }
}
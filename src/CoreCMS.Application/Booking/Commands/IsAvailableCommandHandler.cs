using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Booking.Commands
{
    public class IsAvailableCommandHandler : BaseDbContextWithMediatorHandler<IsAvailableCommand, bool>
    {
        public IsAvailableCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override Task<bool> Handle(IsAvailableCommand request, CancellationToken cancellationToken)
        {
            if(request.StartDate > request.EndDate)
                return Task.FromResult(false);

            var query = _context.BookingView.Where(o => !o.IsDeleted && !o.InActiveStatus 
                && o.RoomId == request.RoomId 
                && request.StartDate < o.BookingEndDate 
                && request.EndDate > o.BookingStartDate);
                
            if(request.BookingId!=null&&request.BookingId!=0){
                query = query.Where(o=>o.BookingId!=request.BookingId);
            }
            var chk = query.Any();
            return Task.FromResult(!chk);
        }
    }
}
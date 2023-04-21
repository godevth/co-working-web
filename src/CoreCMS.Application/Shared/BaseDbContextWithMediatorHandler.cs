using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Shared
{
    public abstract class BaseDbContextWithMediatorHandler<TRequest, TResponse> : BaseDbContextHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly IMediator _mediator;

        public BaseDbContextWithMediatorHandler(ApplicationDbContext context, IMediator mediator) : base(context)
        {
            _mediator = mediator;
        }
    }
}
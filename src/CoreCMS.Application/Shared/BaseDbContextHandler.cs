using CoreCMS.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.Shared
{
    
    public delegate Task<bool> TransactionScopeDelegate(ApplicationDbContext ctx);

    public delegate Task CacheScopeDelegate(Exception ex);

    public abstract class BaseDbContextHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly ApplicationDbContext _context;

        public BaseDbContextHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

        public async Task<bool> InTransactionAsync(TransactionScopeDelegate transactionScope, CacheScopeDelegate cacheScope)
        {
            bool success = false;
            var nestedTrans = _context.Database.CurrentTransaction != null;
            using (var ts = !nestedTrans ? _context.Database.BeginTransaction() : null)
            {
                try
                {
                    if (transactionScope != null)
                    {
                        success = await transactionScope(_context);
                        if (success)
                        {
                            if (!nestedTrans)
                                ts.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!nestedTrans)
                    {
                        ts.Rollback();
                    }

                    if (cacheScope != null)
                    {
                        await cacheScope(ex);
                    }
                }
            }
            return success;
        }
    }
   
}

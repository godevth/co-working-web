using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Place.Commands
{
    public class ToggleWishlistCommandHandler : BaseDbContextWithMediatorHandler<ToggleWishlistCommand,CommandResult<bool>>
    {
        public ToggleWishlistCommandHandler(ApplicationDbContext context, IMediator mediator) :base(context,mediator)
        {
            
        }

        public override async Task<CommandResult<bool>> Handle(ToggleWishlistCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = new CommandResult<bool>();
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถบันทึกข้อมูลได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var wish = _context.WishlistUserMapping.Where(o => o.PlaceId.ToString() == request.PlaceId && o.UserId == request.UserId).FirstOrDefault();
                    if(wish == null)
                    {
                        WishlistUserMapping wishlist = new WishlistUserMapping()
                        {
                            PlaceId = Guid.Parse(request.PlaceId),
                            UserId = request.UserId,
                            IsWishlist = true,
                            CreatedDate = DateTime.Now,
                        };
                        _context.WishlistUserMapping.Add(wishlist);
                        var res = _context.SaveChanges();
                        if(res > 0)
                        {
                            trx.Commit();
                            cmdResult.Message = "บันทึกข้อมูลสำเร็จ";
                            cmdResult.Status = true;
                            cmdResult.Data = wishlist.IsWishlist;
                        }
                    }
                    else
                    {
                        wish.IsWishlist = !wish.IsWishlist;
                        wish.UpdatedDate = DateTime.Now;
                        _context.WishlistUserMapping.Update(wish);
                        var res = _context.SaveChanges();
                        if(res > 0)
                        {
                            trx.Commit();
                            cmdResult.Message = "บันทึกข้อมูลสำเร็จ";
                            cmdResult.Status = true;
                            cmdResult.Data = wish.IsWishlist;
                        }
                    }
                }
                catch (Exception e)
                {
                    trx.Rollback();
                    cmdResult.Message = e.Message;
                }
            }

            return await Task.FromResult(cmdResult);
        }
    }
}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Picture.Queries;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Place.Commands
{
    public class NotApprovePlaceCommandHandler : BaseDbContextWithMediatorHandler<NotApprovePlaceCommand, CommandResult<bool>>
    {
        public NotApprovePlaceCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<CommandResult<bool>> Handle(NotApprovePlaceCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถบันทึกข้อมูลได้"
            };

            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try
                {
                    var query = _context.Place.Where(o => (!o.IsApproveCreate || o.IsApproveDelete ) && o.PlaceId == request.Id).FirstOrDefault();
                    if(request.ApproveStatus)
                    {
                        query.IsApproveDelete = false;
                        query.UpdatedDate = DateTime.Now;
                        query.UpdatedUserId = request.UpdateeUserId;
                    }
                    if(!request.ApproveStatus)
                    {
                        query.IsDeleted = true;
                        query.UpdatedDate = DateTime.Now;
                        query.UpdatedUserId = request.UpdateeUserId;
                    }
                    _context.Update(query);
                    var status = _context.SaveChanges();
                    if(status > 0 )
                    {
                        cmdResult.Message = "บันทึกข้อมูลสำเร็จ";
                        cmdResult.Status = true;
                        cmdResult.Data = true;
                    }
                    trx.Commit ();
                }
                catch (Exception e) {
                    trx.Rollback ();
                    cmdResult.Message = e.Message;
                }
            }
            return await Task.FromResult(cmdResult);
        }
    }
}
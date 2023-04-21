using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCMS.Application.Notification.Commands
{
    public class ResetBadgeCommandHandler : BaseDbContextWithMediatorHandler<ResetBadgeCommand, CommandResult<bool>>
    {
        public ResetBadgeCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }
        public override async Task<CommandResult<bool>> Handle(ResetBadgeCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถแก้ไขข้อมูล Bage ได้"
            };
            using (var ts = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var getDevice = _context.Badges.Where(o => o.UserId == request.UserId).ToList();
                    if (getDevice != null && getDevice.Count > 0)
                    {
                        foreach (var de in getDevice)
                        {
                            if (de.Count != 0)
                            {
                                de.Count = 0;
                                _context.Update(de);
                            }
                        }
                    }

                    var res = await _context.SaveChangesAsync();
                    ts.Commit();
                    cmdResult.Status = res > 0;
                    if (cmdResult.Status)
                    {
                        cmdResult.Data = true;
                        cmdResult.Message = "อัพเดท Bage สำเร็จ";
                    }

                    return cmdResult;
                }
                catch (Exception ex)
                {
                    ts.Rollback();

                    cmdResult.Message = ex.Message;
                    return cmdResult;
                }
            }
        }
    }
}

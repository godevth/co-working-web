using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.TermAndCondition.Commands
{
    public class ToggleActiveStatusTermAndConditionCommandHandler : BaseDbContextWithMediatorHandler<ToggleActiveStatusTermAndConditionCommand, CommandResult<bool>>
    {
        public ToggleActiveStatusTermAndConditionCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override Task<CommandResult<bool>> Handle(ToggleActiveStatusTermAndConditionCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถปรับปรุงสถานะการใช้งานได้"
            };
            var nestedTrans = _context.Database.CurrentTransaction != null;
            using (var ts = !nestedTrans ? _context.Database.BeginTransaction() : null)
            {
                try
                {
                    var term = _context.TermAndCondition.Where(o => o.TermId == request.TermId).Single();
                    term.InActiveStatus = !term.InActiveStatus;
                    term.UpdatedDate = DateTime.Now;
                    term.UpdatedUserId = request.UpdatedUserId;

                    #region Update Other TermAndCondition
                    if(!term.InActiveStatus)
                    {
                        var terms = _context.TermAndCondition.Where(o => !o.InActiveStatus && o.PlaceId == term.PlaceId).ToList();
                        if(terms.Any())
                        {
                            foreach(var t in terms)
                            {
                                t.InActiveStatus = !t.InActiveStatus;
                                t.UpdatedDate = DateTime.Now;
                                t.UpdatedUserId = request.UpdatedUserId;
                                _context.Update(t);
                            }
                        }
                    }
                    #endregion

                    var status = _context.SaveChanges();
                    if(status > 0 )
                    {
                        if (!nestedTrans)
                            ts.Commit();
                            
                        cmdResult.Message = "ปรับปรุงสถานะการใช้งานสำเร็จ";
                        cmdResult.Status = true;
                        cmdResult.Data = true;
                    }
                }
                catch(Exception e)
                {
                    if (!nestedTrans)
                        ts.Rollback();
                        
                    cmdResult.Message = e.Message;
                }
            }
            return Task.FromResult(cmdResult);
        }
    }
}
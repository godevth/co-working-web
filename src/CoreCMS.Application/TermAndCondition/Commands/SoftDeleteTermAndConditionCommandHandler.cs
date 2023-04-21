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
    public class SoftDeleteTermAndConditionCommandHandler : BaseDbContextWithMediatorHandler<SoftDeleteTermAndConditionCommand, CommandResult<bool>>
    {
        public SoftDeleteTermAndConditionCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override Task<CommandResult<bool>> Handle(SoftDeleteTermAndConditionCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถลบข้อมูลได้"
            };

            var term = _context.TermAndCondition.Where(o => o.TermId == request.TermId).Single();
            term.IsDeleted = true;
            term.DeletedDate = DateTime.Now;
            term.DeletedUserId = request.DeleteUserId;

            var status = _context.SaveChanges();
            if(status > 0 )
            {
                cmdResult.Message = "ลบข้อมูลสำเร็จ";
                cmdResult.Status = true;
                cmdResult.Data = true;
            }
            return Task.FromResult(cmdResult);
        }
    }
}
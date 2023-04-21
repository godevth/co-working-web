using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Privilege.Commands
{
    public class SoftDeletePrivilegeCommandHandler : BaseDbContextWithMediatorHandler<SoftDeletePrivilegeCommand, CommandResult<bool>>
    {
        public SoftDeletePrivilegeCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override Task<CommandResult<bool>> Handle(SoftDeletePrivilegeCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถลบข้อมูลได้"
            };

            var privilege = _context.Privilege.Where(o => o.PrivilegeId == request.PrivilegeId).Single();
            privilege.IsDeleted = true;
            privilege.DeletedDate = DateTime.Now;
            privilege.DeletedUserId = request.DeleteUserId;

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
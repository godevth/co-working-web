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
    public class ToggleActiveStatusPrivilegeCommandHandler : BaseDbContextWithMediatorHandler<ToggleActiveStatusPrivilegeCommand, CommandResult<bool>>
    {
        public ToggleActiveStatusPrivilegeCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override Task<CommandResult<bool>> Handle(ToggleActiveStatusPrivilegeCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถปรับปรุงสถานะการใช้งานได้"
            };

            var privilege = _context.Privilege.Where(o => o.PrivilegeId == request.PrivilegeId).Single();
            privilege.InActiveStatus = !privilege.InActiveStatus;
            privilege.UpdatedDate = DateTime.Now;
            privilege.UpdatedUserId = request.UpdatedUserId;

            var status = _context.SaveChanges();
            if(status > 0 )
            {
                cmdResult.Message = "ปรับปรุงสถานะการใช้งานสำเร็จ";
                cmdResult.Status = true;
                cmdResult.Data = true;
            }
            return Task.FromResult(cmdResult);
        }
    }
}
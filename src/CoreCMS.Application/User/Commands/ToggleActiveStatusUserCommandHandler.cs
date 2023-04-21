using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;

namespace CoreCMS.Application.User.Commands
{
    public class ToggleActiveStatusUserCommandHandler : BaseDbContextHandler<ToggleActiveStatusUserCommand, CommandResult<bool>>
    {
        public ToggleActiveStatusUserCommandHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<CommandResult<bool>> Handle(ToggleActiveStatusUserCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถปรับปรุงสถานะการใช้งานได้"
            };

            var user = _context.Users.Where(o => o.Id == request.UserId).Single();
            user.InActiveStatus = !user.InActiveStatus;
            user.UpdatedDate = DateTime.Now;
            user.UpdatedUserId = request.UpdatedUserId;

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
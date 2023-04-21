using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.PlaceTheme.Commands
{
    public class SoftDeletePlaceThemeCommandHandler : BaseDbContextWithMediatorHandler<SoftDeletePlaceThemeCommand, CommandResult<bool>>
    {
        public SoftDeletePlaceThemeCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override Task<CommandResult<bool>> Handle(SoftDeletePlaceThemeCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถลบข้อมูลได้"
            };

            var theme = _context.PlaceTheme.Where(o => o.ThemeId == request.ThemeId).Single();
            theme.IsDeleted = true;
            theme.DeletedDate = DateTime.Now;
            theme.DeletedUserId = request.DeleteUserId;

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
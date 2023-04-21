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
    public class ToggleActiveStatusPlaceThemeCommandHandler : BaseDbContextWithMediatorHandler<ToggleActiveStatusPlaceThemeCommand, CommandResult<bool>>
    {
        public ToggleActiveStatusPlaceThemeCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override Task<CommandResult<bool>> Handle(ToggleActiveStatusPlaceThemeCommand request, CancellationToken cancellationToken)
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
                    var theme = _context.PlaceTheme.Where(o => o.ThemeId == request.ThemeId).Single();
                    theme.InActiveStatus = !theme.InActiveStatus;
                    theme.UpdatedDate = DateTime.Now;
                    theme.UpdatedUserId = request.UpdatedUserId;

                    #region Update Other Theme
                    if(!theme.InActiveStatus)
                    {
                        var themes = _context.PlaceTheme.Where(o => !o.InActiveStatus && o.PlaceId == theme.PlaceId).ToList();
                        if(themes.Any())
                        {
                            foreach(var t in themes)
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
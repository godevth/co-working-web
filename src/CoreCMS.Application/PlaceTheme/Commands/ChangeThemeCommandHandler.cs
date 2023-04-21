using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.PlaceTheme.Queries;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.PlaceTheme.Commands
{
    public class ChangeThemeCommandHandler : BaseDbContextWithMediatorHandler<ChangeThemeCommand, CommandResult<bool>>
    {
        public ChangeThemeCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<CommandResult<bool>> Handle(ChangeThemeCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถเปลี่ยนธีมได้"
            };

            var nestedTrans = _context.Database.CurrentTransaction != null;
            using (var ts = !nestedTrans ? _context.Database.BeginTransaction() : null)
            {
                try
                {
                    if(request.ThemeId != 0)
                    {
                        GetPlaceThemeByUserIdQuery themeQuery = new GetPlaceThemeByUserIdQuery()
                        {
                            UserId = request.CurrentUserId,
                            Page = 1,
                            ItemsPerPage = 10,
                            Language = "TH",
                        };
                        var themes = await _mediator.Send(themeQuery);
                        if(themes == null || themes.Total == 0)
                        {
                            return await Task.FromResult(cmdResult);
                        }
                        
                        bool chk = themes.Items.Where(o => o.ThemeId == request.ThemeId).Any();
                        if(!chk)
                        {
                            return await Task.FromResult(cmdResult);
                        }
                    }

                    #region Update Old Theme 
                    var userThemeUp = _context.UserTheme.Where(o => !o.IsDeleted && !o.InActiveStatus 
                        && o.UserId == request.CurrentUserId)
                        .OrderByDescending(o => o.CreatedDate)
                        .FirstOrDefault();
                    if(userThemeUp != null)
                    {
                        userThemeUp.IsDeleted = true;
                        userThemeUp.InActiveStatus = true;
                        userThemeUp.DeletedUserId = request.CurrentUserId;
                        userThemeUp.DeletedDate = DateTime.Now;

                        _context.Update(userThemeUp);
                    }
                    #endregion

                    Domain.UserTheme userThemeAdd = new Domain.UserTheme()
                    {
                        ThemeId = request.ThemeId != 0 ? request.ThemeId : (int?)null,
                        UserId = request.CurrentUserId,
                        CreatedUserId = request.CurrentUserId,
                        CreatedDate = DateTime.Now
                    };
                    _context.Add(userThemeAdd);
                    
                    var res = await _context.SaveChangesAsync();
                    cmdResult.Status = res > 0;
                    if(cmdResult.Status)
                    {
                        if (!nestedTrans)
                            ts.Commit();
                            
                        cmdResult.Data = true;
                        cmdResult.Message = "เปลี่ยนธีมสำเร็จ";
                    }
                }
                catch (Exception e)
                {
                    if (!nestedTrans)
                        ts.Rollback();

                    cmdResult.Message = e.Message;
                }
            }
            
            return await Task.FromResult(cmdResult);
        }
    }
}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.UserType.Commands
{
    public class ToggleUserTypeCommandHandler : BaseDbContextWithMediatorHandler<ToggleUserTypeCommand,CommandResult<bool>>
    {
        public ToggleUserTypeCommandHandler(ApplicationDbContext context, IMediator mediator) :base(context,mediator)
        {
            
        }

        public override async Task<CommandResult<bool>> Handle(ToggleUserTypeCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = new CommandResult<bool>();
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถบันทึกข้อมูลได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var user = _context.UserType.Where(o => o.UserTypeId == request.UserTypeId).FirstOrDefault();
                    user.InActiveStatus = !user.InActiveStatus;
                    _context.UserType.Update(user);
                    _context.SaveChanges();
                    trx.Commit();
                }
                catch (Exception e)
                {
                    trx.Rollback();
                    cmdResult.Message = e.Message;
                }
            }

            return await Task.FromResult(cmdResult);
        }
    }
}
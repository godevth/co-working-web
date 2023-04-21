using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.UserType.Commands
{
    public class DeleteUserTypeCommandHandler : BaseDbContextWithMediatorHandler<DeleteUserTypeCommand, CommandResult<bool>>
    {
        public DeleteUserTypeCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<CommandResult<bool>> Handle(DeleteUserTypeCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถลบข้อมูลได้"
            };

            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try
                {
                    var Obj = _context.UserType.Where(o => o.UserTypeId == request.Id).Single();
                    var validUserType = _context.Roles.Where(o => o.UserTypeId == request.Id).Any();
                    if (validUserType)
                    {
                        cmdResult.Status = false;
                        cmdResult.Data = false;
                        cmdResult.Message = "ไม่สามารถลบประเภทผู้ใช้งาน " + Obj.Name + " เนื่องจากมีการใช้งานอยู่";
                        return await Task.FromResult(cmdResult);
                    }

                    _context.Remove(Obj);
                    
                    var status = _context.SaveChanges();
                    if(status > 0 )
                    {
                        cmdResult.Message = "ลบข้อมูลสำเร็จ";
                        cmdResult.Status = true;
                        cmdResult.Data = true;
                    }
                    trx.Commit ();
                }
                catch (Exception e) {
                    trx.Rollback ();
                    cmdResult.Message = e.Message;
                }
            }
            return await Task.FromResult(cmdResult);
        }
    }
}
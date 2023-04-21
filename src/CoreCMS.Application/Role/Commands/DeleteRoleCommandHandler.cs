using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Role.Commands
{
    public class DeleteRoleCommandHandler : BaseDbContextWithMediatorHandler<DeleteRoleCommand, CommandResult<bool>>
    {
        public DeleteRoleCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<CommandResult<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถลบข้อมูลได้"
            };

            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try
                {
                    var Obj = _context.Roles.Where(o => o.Id == request.Id).Single();
                    var validRole = _context.UserRoles.Where(o => o.RoleId == request.Id).Any();
                    if (validRole)
                    {
                        cmdResult.Status = false;
                        cmdResult.Data = false;
                        cmdResult.Message = "ไม่สามารถลบสิทธิ์ผู้ใช้งาน " + Obj.Name + " เนื่องจากมีการใช้งานอยู่";
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
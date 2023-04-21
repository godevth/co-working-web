using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Picture.Queries;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Facility.Commands
{
    public class SoftDeleteFacilityCommandHandler : BaseDbContextWithMediatorHandler<SoftDeleteFacilityCommand, CommandResult<bool>>
    {
        public SoftDeleteFacilityCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<CommandResult<bool>> Handle(SoftDeleteFacilityCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถลบข้อมูลได้"
            };

            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try
                {
                    var Obj = _context.Facility.Where(o => o.FacilityId == request.Id).Single();
                    var validFacility = _context.FacilityServices.Where(o => o.FacilityId == request.Id && !o.IsDeleted).Any();
                    if (validFacility)
                    {
                        cmdResult.Status = false;
                        cmdResult.Data = false;
                        cmdResult.Message = "ไม่สามารถลบสิ่งอำนวยความสะดวก " + Obj.FacilityName_TH + " เนื่องจากมีการใช้งานอยู่";
                        return await Task.FromResult(cmdResult);
                    }
                    Obj.IsDeleted = true;
                    Obj.DeletedDate = DateTime.Now;
                    Obj.DeletedUserId = request.DeleteUserId;
                    
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
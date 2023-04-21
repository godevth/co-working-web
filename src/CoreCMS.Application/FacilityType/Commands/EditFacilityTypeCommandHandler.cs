using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;

namespace CoreCMS.Application.FacilityType.Commands
{
    public class EditFacilityTypeCommandHandler : BaseDbContextHandler<EditFacilityTypeCommand, CommandResult<bool>>
    {
        public EditFacilityTypeCommandHandler(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<CommandResult<bool>> Handle(EditFacilityTypeCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถแก้ไขข้อมูลประเภทสถานที่ได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try {
                     if(_context.FacilityType.Where(o=>(o.FacilityTypeName_TH == request.Name_TH||o.FacilityTypeName_EN==request.Name_EN) && o.FacilityTypeID != request.Id &&!o.IsDeleted).Any()){
                       cmdResult.Message = "ไม่สามารถเปลี่ยนข้อมูลประเภทสถานที่ได้ เนื่องจากมีชื่อประเภทสถานที่นี้แล้ว";
                       cmdResult.Data = false;
                     return await Task.FromResult<CommandResult<bool>>(cmdResult);
                   }
                    var getFacilityType = _context.FacilityType.Where(o=>o.FacilityTypeID == request.Id).Single();

                        getFacilityType.FacilityTypeName_TH = request.Name_TH;
                        getFacilityType.FacilityTypeName_EN = request.Name_EN;
                        getFacilityType.InActiveStatus = request.InActiveStatus;
                        getFacilityType.UpdatedUserId = request.UpdateUserId;
                        getFacilityType.UpdatedDate = DateTime.Now;
                    
                    _context.Update(getFacilityType);
                    var res = await _context.SaveChangesAsync();
                    
                    cmdResult.Status = res > 0;
                    if(cmdResult.Status)
                    {
                        cmdResult.Data = true;
                        cmdResult.Message = "แก้ไขข้อมูลประเภทสถานที่สำเร็จ";
                    }
                    trx.Commit ();
                }
                catch (Exception e) {
                    trx.Rollback ();
                    cmdResult.Message = e.Message;
                }
            }
          return await Task.FromResult<CommandResult<bool>>(cmdResult);
        }
    }
}
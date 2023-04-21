using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;

namespace CoreCMS.Application.PlaceType.Commands
{
    public class EditPlaceTypeCommandHandler : BaseDbContextHandler<EditPlaceTypeCommand, CommandResult<bool>>
    {
        public EditPlaceTypeCommandHandler(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<CommandResult<bool>> Handle(EditPlaceTypeCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถแก้ไขข้อมูลประเภทสถานที่ได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try {
                     if(_context.PlaceType.Where(o=>o.PlaceTypeName == request.Name &&o.PlaceTypeID != request.Id &&!o.IsDeleted).Any()){
                       cmdResult.Message = "ไม่สามารถเปลี่ยนข้อมูลประเภทสถานที่ได้ เนื่องจากมีชื่อประเภทสถานที่นี้แล้ว";
                       cmdResult.Data = false;
                     return await Task.FromResult<CommandResult<bool>>(cmdResult);
                   }
                    var getLocation = _context.PlaceType.Where(o=>o.PlaceTypeID == request.Id).Single();

                        getLocation.PlaceTypeName = request.Name;
                        getLocation.PlaceTypeNameEN = request.NameEN;
                        getLocation.InActiveStatus = request.InActiveStatus;
                        getLocation.UpdatedUserId = request.UpdateUserId;
                        getLocation.UpdatedDate = DateTime.Now;
                    
                    _context.Update(getLocation);
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
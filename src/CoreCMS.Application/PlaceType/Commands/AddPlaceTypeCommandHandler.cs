using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;

namespace CoreCMS.Application.PlaceType.Commands
{
    public class AddPlaceTypeCommandHandler : BaseDbContextHandler<AddPlaceTypeCommand, CommandResult<bool>>
    {
        public AddPlaceTypeCommandHandler(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<CommandResult<bool>> Handle(AddPlaceTypeCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถสร้างข้อมูลประเภทสถานที่ได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try {
                   if(_context.PlaceType.Where(o=>o.PlaceTypeName == request.Name && !o.IsDeleted).Any()){
                       cmdResult.Message = "ไม่สามารถสร้างข้อมูลประเภทสถานที่ได้ เนื่องจากมีชื่อประเภทสถานที่นี้แล้ว";
                       cmdResult.Data = false;
                     return await Task.FromResult<CommandResult<bool>>(cmdResult);
                   }
                    Domain.PlaceType menu = new Domain.PlaceType()
                    {
                        PlaceTypeName = request.Name,
                        PlaceTypeNameEN = request.NameEN,
                        InActiveStatus = request.InActiveStatus,
                        CreatedUserId = request.CreateUserId,
                        CreatedDate = DateTime.Now
                    };
                    
                    _context.Add(menu);
                    var res = await _context.SaveChangesAsync();
                    
                    cmdResult.Status = res > 0;
                    if(cmdResult.Status)
                    {
                        cmdResult.Data = true;
                        cmdResult.Message = "สร้างข้อมูลประเภทสถานที่สำเร็จ";
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
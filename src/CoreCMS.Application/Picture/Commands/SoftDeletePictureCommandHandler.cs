using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;

namespace CoreCMS.Application.Picture.Commands
{
    public class SoftDeletePictureCommandHandler : BaseDbContextHandler<SoftDeletePictureCommand, CommandResult<bool>>
    {
        public SoftDeletePictureCommandHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<CommandResult<bool>> Handle(SoftDeletePictureCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถลบข้อมูลได้"
            };

            var pictures = _context.Picture.Where(o => o.CodeRef == request.CodeRef && o.TypeRef == request.TypeRef && o.FileInfoId == request.FileInfoId).ToList();
            foreach (var pic in pictures)
            {
                pic.IsDeleted = true;
                pic.InActiveStatus = true;
                pic.UpdatedDate = DateTime.Now;
                pic.UpdatedUserId = request.DeleteUserId;

                _context.Update(pic);
            }
            _context.SaveChanges();
            
            cmdResult.Message = "ลบข้อมูลสำเร็จ";
            cmdResult.Status = true;
            cmdResult.Data = true;

            return Task.FromResult(cmdResult);
        }
    }
}
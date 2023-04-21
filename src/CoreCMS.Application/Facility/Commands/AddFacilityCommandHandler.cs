using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Picture.Commands;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoreCMS.Application.Facility.Commands
{
    public class AddFacilityCommandHandler : BaseDbContextHandler<AddFacilityCommand, CommandResult<bool>>
    {
        protected IMediator _mediator;
        public AddFacilityCommandHandler(ApplicationDbContext context,IMediator mediator) : base(context)
        {
            _mediator = mediator;
        }
        public override async Task<CommandResult<bool>> Handle(AddFacilityCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถสร้างข้อมูลสถานที่ได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try {
                   if(_context.Facility.Where(o=>(o.FacilityName_TH == request.Name_TH ||o.FacilityName_EN == request.Name_EN) &&!o.IsDeleted ).Any()){
                       cmdResult.Message = "ไม่สามารถสร้างข้อมูลสถานที่ได้ เนื่องจากมีชื่อสถานที่นี้แล้ว";
                       cmdResult.Data = false;
                     return await Task.FromResult<CommandResult<bool>>(cmdResult);
                   }
                    Domain.Facility data = new Domain.Facility()
                    {
                        FacilityName_TH = request.Name_TH,
                        FacilityName_EN = request.Name_EN,
                        FacilityTypeID = Convert.ToInt32(request.FacilityTypeId),
                        InActiveStatus = request.InActiveStatus,
                        CreatedUserId = request.CreateUserId,
                        CreatedDate = DateTime.Now
                    };
                    
                    _context.Add(data);
                    var res = await _context.SaveChangesAsync();

                     #region AddPicture
                    if (request.Pictures!=null&&request.Pictures.Count>0)
                    {
                        foreach (var item in request.Pictures) {
                            AddPictureCommand addPictureCmd = new AddPictureCommand()
                            {
                                FileName = Guid.NewGuid().ToString(),
                                CodeRef = data.FacilityId.ToString(),
                                TypeRef = PictureTypeRef.Facility,
                                ContentType = item.ContentTypes,
                                FileBase64 = item.Base64s,
                                CreateUserId = request.CreateUserId
                            };
                            var picResult = await _mediator.Send(addPictureCmd);
                            int errorSize = picResult.Errors?.Count ?? 0; //picResult.Errors == null ? 0 : picResult.Errors.Count
                            if (!picResult.Status)
                            {
                                if (errorSize > 0)
                                    throw new Exception($"{string.Join(", ", picResult.Errors.Select(o => o.Value.Join(", ")))}");
                                else
                                    throw new Exception(picResult.Message);
                            }
                        }
                        
                    }
                    #endregion
                    
                    cmdResult.Status = res > 0;
                    if(cmdResult.Status)
                    {
                        cmdResult.Data = true;
                        cmdResult.Message = "สร้างข้อมูลสถานที่สำเร็จ";
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
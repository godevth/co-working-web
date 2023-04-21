using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using Domain = CoreCMS.Domain;
using CoreCMS.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.ApiMasterFile.Commands;
using CoreCMS.Application.Picture.Commands;
using CoreCMS.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoreCMS.Application.Place.Commands
{
    public class EditPlaceCommandHandler : BaseDbContextHandler<EditPlaceCommand, CommandResult<bool>>
    {
         protected IMediator _mediator;
        public EditPlaceCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context)
        {
            _mediator = mediator;
        }

        public async override Task<CommandResult<bool>> Handle(EditPlaceCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถสร้างข้อมูลสถานที่ได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (_context.Place.Where(o => (o.PlaceName_TH == request.Name_TH || o.PlaceName_EN == request.Name_EN) && o.PlaceId != request.PlaceId &&!o.IsDeleted).Any())
                    {
                        cmdResult.Message = "ไม่สามารถสร้างข้อมูลประเภทสถานที่ได้ เนื่องจากมีชื่อสถานที่นี้แล้ว";
                        cmdResult.Data = false;
                        return await Task.FromResult<CommandResult<bool>>(cmdResult);
                    }
                    var query = _context.Place.Where(o => o.PlaceId == request.PlaceId).Single();

                    query.PlaceName_TH = request.Name_TH;
                    query.PlaceName_EN = request.Name_EN;
                    query.Address = request.Address;
                    query.TAMBON_ID = Convert.ToInt32(request.TambolId);
                    query.AMPER_ID = Convert.ToInt32(request.AmpherId);
                    query.PROVINCE_ID = Convert.ToInt32(request.ProvinceId);
                    query.ZIP_CODE = request.Zipcode;
                    query.Longitude = request.Longitude;
                    query.Latitude = request.Latitude;
                    query.InActiveStatus = request.InActiveStatus;
                    query.UpdatedUserId = request.UpdateUserId;
                    query.UpdatedDate = DateTime.Now;
                    query.NearBy = request.NearBy;
                    query.Details = request.Details;
                    query.CompanyId =Convert.ToInt32(request.CompanyId);
                    query.PlaceTypeID = Convert.ToInt32(request.PlaceTypeId);
                    query.SeeingTypeCode = request.Seeing;


                    if (request.DateItems != null && request.DateItems.Count > 0)
                    {
                        List<Domain.ImplementationDate> listIm = new List<Domain.ImplementationDate>();
                        var getItem = _context.ImplementationDate.Where(o => o.PlaceId == request.PlaceId && !o.IsDeleted).ToList();
                        var setItems = request.DateItems.Where(o => o.ImplementationDateId != null).Select(o => o.ImplementationDateId).ToList();
                        var add = request.DateItems.Where(o => o.ImplementationDateId == null).ToList();
                        var del = getItem.Where(o => !setItems.Contains(o.ImplementationDateId)).ToList();
                        var up = request.DateItems.Where(o => o.ImplementationDateId != null).ToList();
                        foreach (var item in add)
                        {
                            listIm.Add(new Domain.ImplementationDate()
                            {
                                StartDay = item.StartDay,
                                StartTime = item.StartTime,
                                EndTime = item.EndTime,
                                CreatedUserId = request.UpdateUserId,
                                InActiveStatus = false,
                                CreatedDate = DateTime.Now
                            });
                        }

                        foreach (var item in up)
                        {
                            var imp = _context.ImplementationDate.Where(o => o.ImplementationDateId == item.ImplementationDateId).Single();
                            imp.StartDay = item.StartDay;
                            imp.StartTime = item.StartTime;
                            imp.EndTime = item.EndTime;
                            imp.UpdatedUserId = request.UpdateUserId;
                            imp.UpdatedDate = DateTime.Now;
                            listIm.Add(imp);

                        }
                        foreach (var item in del)
                        {
                            item.IsDeleted = true;
                            item.DeletedUserId = request.UpdateUserId;
                            item.DeletedDate = DateTime.Now;
                            listIm.Add(item);
                        }


                        query.ImplementationDate = listIm;
                    }
                    else {
                        var allItem = _context.ImplementationDate.Where(o => o.PlaceId == request.PlaceId && !o.IsDeleted).ToList();
                        if(allItem.Count>0){
                        foreach (var item in allItem) {
                            item.IsDeleted = true;
                            item.DeletedUserId = request.UpdateUserId;
                            item.DeletedDate = DateTime.Now;  
                        }
                        _context.UpdateRange(allItem);
                        }
                    }
                    if (request.ServiceItems != null && request.ServiceItems.Count > 0)
                    {
                        List<Domain.FacilityServices> listF = new List<Domain.FacilityServices>();
                        var getItem = _context.FacilityServices.Where(o => o.PlaceId == request.PlaceId && !o.IsDeleted).ToList();
                        var setItems = request.ServiceItems.Where(o => o.FacilityServicesId != null).Select(o => o.FacilityServicesId).ToList();
                        var add = request.ServiceItems.Where(o => o.FacilityServicesId == null).ToList();
                        var del = getItem.Where(o => !setItems.Contains(o.FacilityServicesId)).ToList();
                        var up = request.ServiceItems.Where(o => o.FacilityServicesId != null).ToList();
                        foreach (var item in add)
                        {
                            listF.Add(new Domain.FacilityServices()
                            {
                                Price = Convert.ToDecimal(item.Price),
                                Qty = Convert.ToInt32(item.Qty),
                                FacilityId = item.FacilityId,
                                CreatedUserId = request.UpdateUserId,
                                InActiveStatus = false,
                                CreatedDate = DateTime.Now
                            });
                        }

                        foreach (var item in up)
                        {
                            var imp = _context.FacilityServices.Where(o => o.FacilityServicesId == item.FacilityServicesId).Single();
                            imp.FacilityId = item.FacilityId;
                            imp.Qty = Convert.ToInt32(item.Qty);
                            imp.Price = Convert.ToDecimal(item.Price);
                            imp.UpdatedUserId = request.UpdateUserId;
                            imp.UpdatedDate = DateTime.Now;
                            listF.Add(imp);

                        }
                        foreach (var item in del)
                        {
                            item.IsDeleted = true;
                            item.DeletedUserId = request.UpdateUserId;
                            item.DeletedDate = DateTime.Now;
                            listF.Add(item);
                        }
                        query.FacilityServices = listF;
                    }
                    else
                    {
                        var allItem = _context.FacilityServices.Where(o => o.PlaceId == request.PlaceId && !o.IsDeleted).ToList();
                        if(allItem.Count>0){
                            foreach (var item in allItem)
                        {
                            item.IsDeleted = true;
                            item.DeletedUserId = request.UpdateUserId;
                            item.DeletedDate = DateTime.Now;
                        }
                        _context.UpdateRange(allItem);
                        }
                    }

                    var paymentMethodsDB = _context.PlacePaymentMethod.Where(o => o.PlaceId == request.PlaceId).ToList();
                    _context.UpdateRange(paymentMethodsDB);
                    if (request.PaymentMethods != null && request.PaymentMethods.Any())
                    {
                        List<Domain.PlacePaymentMethod> placePaymentMethods = new List<Domain.PlacePaymentMethod>();
                        foreach(var p in request.PaymentMethods)
                        {
                            placePaymentMethods.Add(new Domain.PlacePaymentMethod()
                            {
                                PaymentMethodCode = p,
                            });
                        }
                        query.PlacePaymentMethods = placePaymentMethods;
                    }

                    var privilegesDB = _context.Privilege.Where(o => !o.IsDeleted && !o.InActiveStatus && o.PlaceId == request.PlaceId).ToList();
                    _context.UpdateRange(privilegesDB);
                    if (request.Domains != null && request.Domains.Any())
                    {
                        List<Domain.Privilege> privileges = new List<Domain.Privilege>();
                        foreach(var d in request.Domains)
                        {
                            privileges.Add(new Domain.Privilege()
                            {
                                Domain = d.Trim(),
                                PrivilegeTypeCode = "PRIVILEGE_TYPE_DOMAIN"
                            });
                        }
                        query.Privileges = privileges;
                    }
                    

                    _context.Update(query);
                    var res = await _context.SaveChangesAsync();

                    #region file set
                    if(request.Pictures != null)
                    {
                        var del = _context.Picture.Where(o => o.TypeRef == PictureTypeRef.UploadFileByPlace && o.CodeRef == request.PlaceId.ToString() && !o.IsDeleted).ToList();
                        foreach (var item in del)
                        {
                            DeleteFileCommand deleteFileCmd = new DeleteFileCommand()
                            {
                                FileInfoId = item.FileInfoId
                            };
                            var deleteFileRes = await _mediator.Send(deleteFileCmd);
                            if (!deleteFileRes)
                                throw new Exception("ลบไฟล์เก่าไม่สำเร็จ");

                            SoftDeletePictureCommand deleteCmd = new SoftDeletePictureCommand()
                            {
                                CodeRef = request.PlaceId.ToString(),
                                TypeRef = PictureTypeRef.UploadFileByPlace,
                                DeleteUserId = request.UpdateUserId,
                                FileInfoId = item.FileInfoId
                            };
                            var softDelete = await _mediator.Send(deleteCmd);
                            if (!softDelete.Status)
                                throw new Exception("ลบไฟล์เก่าไม่สำเร็จ");
                        }

                        foreach (var item in request.Pictures) {
                            AddPictureCommand addPictureCmd = new AddPictureCommand()
                            {
                                FileName = item.FileName,
                                CodeRef = request.PlaceId.ToString(),
                                TypeRef = PictureTypeRef.UploadFileByPlace,
                                ContentType = item.ContentTypes,
                                FileBase64 = item.Base64s,
                                CreateUserId = query.CreatedUserId
                            };
                            var picResult = await _mediator.Send(addPictureCmd);
                            int errorSize = picResult.Errors?.Count ?? 0;
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
                    if (cmdResult.Status)
                    {
                        cmdResult.Data = true;
                        cmdResult.Message = "แก้ไขข้อมูลสถานที่สำเร็จ";
                    }
                    trx.Commit();
                }
                catch (Exception e)
                {
                    trx.Rollback();
                    cmdResult.Message = e.Message;
                }
            }
            return await Task.FromResult<CommandResult<bool>>(cmdResult);
        }
    }
}

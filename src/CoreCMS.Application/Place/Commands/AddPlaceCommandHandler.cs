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
using CoreCMS.Application.Picture.Commands;
using MediatR;
using CoreCMS.Domain;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoreCMS.Application.Place.Commands
{
   public class AddPlaceCommandHandler : BaseDbContextHandler<AddPlaceCommand, CommandResult<bool>>
    {
        protected IMediator _mediator;
        public AddPlaceCommandHandler(ApplicationDbContext context,IMediator mediator) : base(context)
        {
             _mediator = mediator;
        }

        public async override Task<CommandResult<bool>> Handle(AddPlaceCommand request, CancellationToken cancellationToken)
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
                    if (_context.Place.Where(o =>!o.IsDeleted && (o.PlaceName_TH == request.Name_TH || o.PlaceName_EN == request.Name_EN)&&o.PlaceTypeID==Convert.ToInt32(request.PlaceTypeId)).Any())
                    {
                        cmdResult.Message = "ไม่สามารถสร้างข้อมูลประเภทสถานที่ได้ เนื่องจากมีชื่อสถานที่นี้แล้ว";
                        cmdResult.Data = false;
                        return await Task.FromResult<CommandResult<bool>>(cmdResult);
                    }
                    Domain.Place place = new Domain.Place()
                    {
                        PlaceName_TH = request.Name_TH,
                        PlaceName_EN =request.Name_EN,
                        Address = request.Address,
                        TAMBON_ID = Convert.ToInt32(request.TambolId),
                        AMPER_ID = Convert.ToInt32(request.AmpherId),
                        PROVINCE_ID = Convert.ToInt32(request.ProvinceId),
                        ZIP_CODE = request.Zipcode,
                        Longitude = request.Longitude,
                        Latitude = request.Latitude,
                        InActiveStatus = request.InActiveStatus,
                        CreatedUserId = request.CreateUserId,
                        CreatedDate = DateTime.Now,
                        PlaceTypeID = Convert.ToInt32(request.PlaceTypeId),
                        NearBy = request.NearBy,
                        Details = request.Details,
                        CompanyId =Convert.ToInt32(request.CompanyId),
                        SeeingTypeCode = request.Seeing,
                    };
                    if (request.DateItems != null && request.DateItems.Count > 0)
                    {
                        List<Domain.ImplementationDate> listIm = new List<Domain.ImplementationDate>();
                        foreach (var item in request.DateItems)
                        {
                            listIm.Add(new Domain.ImplementationDate()
                            {
                                StartDay= item.StartDay,
                                StartTime = item.StartTime,
                                EndTime = item.EndTime,
                                CreatedUserId = request.CreateUserId,
                                InActiveStatus = false,
                                CreatedDate = DateTime.Now
                            });
                        }
                        place.ImplementationDate = listIm;
                    }
                    if (request.ServiceItems != null && request.ServiceItems.Count > 0)
                    {
                        List<Domain.FacilityServices> listF = new List<Domain.FacilityServices>();
                        foreach (var item in request.ServiceItems)
                        {
                            listF.Add(new Domain.FacilityServices()
                            {
                                Price = Convert.ToDecimal(item.Price),
                                Qty = Convert.ToInt32(item.Qty),
                                FacilityId = item.FacilityId,
                                CreatedUserId = request.CreateUserId,
                                InActiveStatus = false,
                                CreatedDate = DateTime.Now
                            });
                        }
                        place.FacilityServices = listF;
                    }

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
                        place.PlacePaymentMethods = placePaymentMethods;
                    }

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
                        place.Privileges = privileges;
                    }

                    _context.Add(place);
                    var res = await _context.SaveChangesAsync();
                    #region AddFile
                    if (request.Pictures!=null&&request.Pictures.Count>0)
                    {
                        foreach (var item in request.Pictures) {
                            AddPictureCommand addPictureCmd = new AddPictureCommand()
                            {
                                FileName = item.FileName,
                                CodeRef = place.PlaceId.ToString(),
                                TypeRef = PictureTypeRef.UploadFileByPlace,
                                ContentType = item.ContentTypes,
                                FileBase64 = item.Base64s,
                                CreateUserId = request.CreateUserId
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
                        cmdResult.Message = "สร้างข้อมูลสถานที่สำเร็จ";
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

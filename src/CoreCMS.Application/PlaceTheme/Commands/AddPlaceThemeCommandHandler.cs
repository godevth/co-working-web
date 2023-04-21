using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Picture.Commands;
using CoreCMS.Application.Place.Queries;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoreCMS.Application.PlaceTheme.Commands
{
    public class AddPlaceThemeCommandHandler : BaseDbContextWithMediatorHandler<AddPlaceThemeCommand, CommandResult<string>>
    {
        public AddPlaceThemeCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<CommandResult<string>> Handle(AddPlaceThemeCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถบันทึกข้อมูลธีมได้"
            };

            var nestedTrans = _context.Database.CurrentTransaction != null;
            using (var ts = !nestedTrans ? _context.Database.BeginTransaction() : null)
            {
                try
                {
                    GetPlaceNameQuery placeNameQuery = new GetPlaceNameQuery()
                    {
                        Id = request.PlaceId
                    };
                    var place = await _mediator.Send(placeNameQuery);
                    if(place == null)
                        throw new Exception("ไม่พบข้อมูลสถานที่");

                    Domain.PlaceTheme placeTheme = new Domain.PlaceTheme()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        PlaceId = place.PlaceId.Value,
                        InActiveStatus = true,
                        CreatedUserId = request.CreateUserId,
                        CreatedDate = DateTime.Now,
                    };
                    _context.Add(placeTheme);
                    var res = await _context.SaveChangesAsync();
                    if(res > 0 && request.Pictures.Any())
                    {
                        foreach(var item in request.Pictures)
                        {
                            string typeRef = "";
                            if(PictureTypeRef.ThemeTypeBgLight == item.Type.ToUpper())
                                typeRef = PictureTypeRef.ThemeTypeBgLight;
                            else if(PictureTypeRef.ThemeTypeBgDark == item.Type.ToUpper())
                                typeRef = PictureTypeRef.ThemeTypeBgDark;
                            else if(PictureTypeRef.ThemeTypeLogoLight == item.Type.ToUpper())
                                typeRef = PictureTypeRef.ThemeTypeLogoLight;
                            else if(PictureTypeRef.ThemeTypeLogoDark == item.Type.ToUpper())
                                typeRef = PictureTypeRef.ThemeTypeLogoDark;

                            if(!string.IsNullOrEmpty(typeRef))
                            {
                                AddPictureCommand addPictureCmd = new AddPictureCommand()
                                {
                                    FileName = Guid.NewGuid().ToString(),
                                    CodeRef = placeTheme.ThemeId.ToString(),
                                    TypeRef = typeRef,
                                    ContentType = item.ContentType,
                                    FileBase64 = item.Base64,
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
                    }
                    cmdResult.Status = res > 0;
                    if(cmdResult.Status)
                    {
                        if (!nestedTrans)
                            ts.Commit();
                            
                        cmdResult.Data = placeTheme.ThemeId.ToString();
                        cmdResult.Message = "บันทึกข้อมูลธีมสำเร็จ";
                    }
                }
                catch(Exception e)
                {
                    if (!nestedTrans)
                        ts.Rollback();
                        
                    cmdResult.Message = e.Message;
                }
            }
            return await Task.FromResult(cmdResult);
        }
    }
}
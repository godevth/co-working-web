using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Place.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.SSO.Commands;
using CoreCMS.Application.SSO.Models;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Place.Commands
{
    public class PlaceQRCodeGeneratorCommandHandler : BaseDbContextWithMediatorHandler<PlaceQRCodeGeneratorCommand, PlaceQRCode>
    {
        public PlaceQRCodeGeneratorCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<PlaceQRCode> Handle(PlaceQRCodeGeneratorCommand request, CancellationToken cancellationToken)
        {
            #region Place
            var place = _context.Place.Where(o => !o.IsDeleted && !o.InActiveStatus && o.PlaceId == request.PlaceId)
                .FirstOrDefault();
            if (place == null)
                throw new Exception("ไม่พบข้อมูลสถานที่");
            #endregion

            #region Gen QR Code
            QRCodeGeneratorCommand qrCodeCmd = new QRCodeGeneratorCommand();
            List<QRInfo> qrInfos = new List<QRInfo>();
            qrInfos.Add(new QRInfo()
            {
                Id = place.PlaceId.ToString(),
                Value = place.PlaceId.ToString(),
                Width = 500,
                Height = 500
            });
            qrCodeCmd.QRInfos = qrInfos;
            var qrCode = await _mediator.Send(qrCodeCmd);
            if(!qrCode.Status)
                throw new Exception("ไม่สามารถ Generate QR Code ได้");

            if(qrCode.Data.Result.Any())
            {
                PlaceQRCode placeQRCode = new PlaceQRCode()
                {
                    FileName = place.PlaceId.ToString(),
                    FileByte = Convert.FromBase64String(qrCode.Data.Result[0].Base64)
                };
                #endregion
                return await Task.FromResult(placeQRCode);
            }
            else 
                throw new Exception("ไม่สามารถ Generate QR Code ได้");
        }
    }
}
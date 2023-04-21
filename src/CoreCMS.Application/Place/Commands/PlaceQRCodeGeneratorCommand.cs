using System;
using CoreCMS.Application.Place.Models;
using MediatR;

namespace CoreCMS.Application.Place.Commands
{
    public class PlaceQRCodeGeneratorCommand : IRequest<PlaceQRCode>
    {
        public Guid PlaceId { get; set; }
    }
}
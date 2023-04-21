using CoreCMS.Application.Place.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Place.Queries
{
   public class GetPlaceDetailQuery : IRequest<CommandResult<PlaceModels>>
    {
        public Guid Id { get; set; }
    }
}

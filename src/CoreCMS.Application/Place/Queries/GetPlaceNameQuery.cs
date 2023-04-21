using System;
using CoreCMS.Application.Place.Models;
using MediatR;

namespace CoreCMS.Application.Place.Queries
{
    public class GetPlaceNameQuery  : IRequest<PlaceModels>
    {
        public Guid Id { get; set; }
    }
}
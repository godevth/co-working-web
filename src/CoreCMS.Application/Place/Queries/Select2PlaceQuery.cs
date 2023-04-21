using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using MediatR;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Place.Queries
{
    public class Select2PlaceQuery : IRequest<OptionViewModel[]>
    {
        public string Search { get; set; }
        public int PlaceTypeId { get; set; }
        public string Language { get; set;}
    }
}

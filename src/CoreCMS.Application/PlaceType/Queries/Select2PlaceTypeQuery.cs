using CoreCMS.Application.PlaceType.Models;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using MediatR;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.PlaceType.Queries
{
    public class Select2PlaceTypeQuery : IRequest<OptionViewModel[]>
    {
        public string Search { get; set; }
        public bool IsAll { get; set; }
        public string Language { get; set;}
    }
}

using CoreCMS.Application.FacilityType.Models;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using MediatR;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Facility.Queries
{
    public class Select2FacilityQuery : IRequest<OptionViewModel[]>
    {
        public string Search { get; set; }
        public bool IsAll { get; set; }
    }
}

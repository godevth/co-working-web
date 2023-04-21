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
    public class Select2FacilityByGroupQuery : IRequest<OptionViewModel[]>
    {
        public string Search { get; set; }
        public string Id { get; set; }
        public string FacilityGroup { get; set; } // 0 ห้อง, 1 สถานที่
        public string Language { get; set; }
    }
}

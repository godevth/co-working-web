using CoreCMS.Application.FacilityType.Models;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using MediatR;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.FacilityType.Queries
{
    public class GetFacilityTypeQuery : IRequest<CommandResult<FacilityTypeView>>
    {
        public int Id { get; set; }
    }
}

using CoreCMS.Application.User.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.User.Queries
{
    public class Select2UserByTypeQuery : IRequest<OptionViewModel[]>
    {
        public string Search { get; set; }
        public int UserType { get; set; }
    }
}

using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using MediatR;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.User.Queries
{
    public class SearchUserQuery : IRequest<SearchResult<UserView>>
    {
         public string[] SortBy { get; set; }

        public bool[] SortDesc { get; set; }

        public int ItemsPerPage { get; set; }

        public int Page { get; set; }

        public string SearchKeyword { get; set; }

        public string Role { get; set; }

        public bool? OpenId { get; set; }
        
        public string UserTypeId { get; set; }
    }
}

using CoreCMS.Application.Privilege.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoreCMS.Application.Privilege.Queries
{
    public class SearchPrivilegeQuery : IRequest<SearchResult<PrivilegeView>>
    {
        public string[] SortBy { get; set; }

        public bool[] SortDesc { get; set; }

        public int ItemsPerPage { get; set; }

        public int Page { get; set; }

        public string SearchKeyword { get; set; }

        [Required]
        public Guid PlaceId { get; set; }

        public string PrivilegeTypeCode { get; set; }
        public bool? InActiveStatus { get; set; }
    }
}
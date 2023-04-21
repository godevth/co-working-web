using System;
using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.TermAndCondition.Models;
using MediatR;

namespace CoreCMS.Application.TermAndCondition.Queries
{
    public class SearchTermAndConditionQuery : IRequest<SearchResult<TermAndConditionView>>
    {
        public string[] SortBy { get; set; }

        public bool[] SortDesc { get; set; }

        public int ItemsPerPage { get; set; }

        public int Page { get; set; }

        public string SearchKeyword { get; set; }

        [Required]
        public Guid PlaceId { get; set; }

        public bool? InActiveStatus { get; set; }
    }
}
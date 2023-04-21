using System;
using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.PlaceTheme.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.PlaceTheme.Queries
{
    public class SearchThemeQuery : IRequest<SearchResult<ThemeView>>
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
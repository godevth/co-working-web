using CoreCMS.Application.Booking.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Booking.Queries
{
   public class HistoryQuery : IRequest<SearchResult<HistoryView>>
    {
        public string Search { get; set; }
        public string UserId { get; set; }
        public string Language { get; set; }
        public string GetStatus { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }
}

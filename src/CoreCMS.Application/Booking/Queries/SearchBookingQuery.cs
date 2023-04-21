using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CoreCMS.Application.Booking.Models;
using CoreCMS.Application.Shared.JsonConverters;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Booking.Queries
{
    public class SearchBookingQuery : IRequest<SearchResult<HistoryView>>
    {
        public string[] SortBy { get; set; }

        public bool[] SortDesc { get; set; }

        public int ItemsPerPage { get; set; }

        public int Page { get; set; }

        public string SearchKeyword { get; set; }

        public Guid? PlaceId { get; set; }

        public Guid? RoomId { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public bool InActiveStatus { get; set; }

        public bool IsDeleted { get; set; }

        public string UserId { get; set; }
        public bool IsSuperAdmin { get; set; }

        public DateTime? StartDateTime 
        { 
            get
            {
                DateTime d;
                if (DateTime.TryParseExact(StartDate,
                        "dd/MM/yyyy",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out d))
                {
                    return d;
                }
                return null;
            }
        }
        
        public DateTime? EndDateTime 
        { 
            get
            {
                DateTime d;
                if (DateTime.TryParseExact(EndDate,
                        "dd/MM/yyyy",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out d))
                {
                    d = d.AddHours(23).AddMinutes(59).AddSeconds(59);
                    return d;
                }
                return null;
            }
        }
    }
}
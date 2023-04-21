using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.ImplementationDate.Models;
using MediatR;

namespace CoreCMS.Application.ImplementationDate.Queries
{
    public class SearchImplementationDateQuery : IRequest<List<ImplementationDateView>>
    {
        public SearchImplementationDateQuery()
        {
            StartDays = new List<string>();
        }

        [Required]
        public Guid PlaceId { get; set; }

        public List<string> StartDays { get; set; }
    }
}
using System.Collections.Generic;
using CoreCMS.Application.Place.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Place.Queries
{
    public class GetWishlistQuery : IRequest<SearchResult<WishlistViewModel>>
    {
        public string UserId { get; set;}
        public string Language { get; set;}
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }
}

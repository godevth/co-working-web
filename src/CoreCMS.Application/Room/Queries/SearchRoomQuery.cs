using CoreCMS.Application.Room.Models;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using MediatR;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Room.Queries
{
    public class SearchRoomQuery : IRequest<SearchResult<RoomView>>
    {
        public RoomDataTable DataTable { get; set; }
    }
}

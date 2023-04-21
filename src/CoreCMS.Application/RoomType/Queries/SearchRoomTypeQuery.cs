using CoreCMS.Application.RoomType.Models;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.User.Models;
using MediatR;
using SBP.Common.Data.Datatables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.RoomType.Queries
{
    public class SearchRoomTypeQuery : IRequest<SearchResult<RoomTypeView>>
    {
        public RoomTypeDataTable DataTable { get; set; }
    }
}

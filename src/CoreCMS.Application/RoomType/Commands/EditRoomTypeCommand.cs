using System.Collections.Generic;
using CoreCMS.Application.Room.Models;
using CoreCMS.Application.RoomType.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.RoomType.Commands
{
    public class EditRoomTypeCommand:IRequest<CommandResult<bool>>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }

        public bool InActiveStatus { get; set; }
        public string UpdateUserId { get; set; }
        public List<Pictures> Picture { get; set;}
    }
}
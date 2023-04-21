using CoreCMS.Application.Room.Models;
using CoreCMS.Application.RoomType.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.RoomType.Commands
{
    public class AddRoomTypeCommand:IRequest<CommandResult<bool>>
    {

        public string Name { get; set; }
        public string NameEN { get; set; }
        public bool InActiveStatus { get; set; }
        public string CreateUserId { get; set; }
        public Pictures Picture { get; set;}
    }

    
}
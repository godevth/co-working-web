using CoreCMS.Application.Room.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Room.Commands
{
    public class AddRoomCommand: RoomModels,IRequest<CommandResult<bool>>
    {
        public string CreateUserId { get; set; }
    }
}
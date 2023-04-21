using System;
using CoreCMS.Application.Room.Models;
using CoreCMS.Application.RoomType.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Room.Commands
{
    public class EditRoomCommand:RoomModels,IRequest<CommandResult<bool>>
    {
        public string UpdateUserId { get; set; }
    }
}
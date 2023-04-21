using System;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Room.Commands
{
    public class SoftDeleteRoomCommand : IRequest<CommandResult<bool>>
    {
        public Guid Id { get; set; }
        public string DeleteUserId { get; set; }
    }
}
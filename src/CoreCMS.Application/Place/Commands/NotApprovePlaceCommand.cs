using System;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Place.Commands
{
    public class NotApprovePlaceCommand : IRequest<CommandResult<bool>>
    {
        public Guid Id { get; set; }
        public bool ApproveStatus { get; set;}
        public string UpdateeUserId { get; set; }
    }
}
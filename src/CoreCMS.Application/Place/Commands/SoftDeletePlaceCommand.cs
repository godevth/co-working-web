using System;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Place.Commands
{
    public class SoftDeletePlaceCommand : IRequest<CommandResult<bool>>
    {
        public Guid Id { get; set; }
        public string DeleteUserId { get; set; }
    }
}
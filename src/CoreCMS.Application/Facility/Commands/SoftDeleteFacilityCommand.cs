using System;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Facility.Commands
{
    public class SoftDeleteFacilityCommand : IRequest<CommandResult<bool>>
    {
        public Guid Id { get; set; }
        public string DeleteUserId { get; set; }
    }
}
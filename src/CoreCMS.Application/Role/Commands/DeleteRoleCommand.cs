using System;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Role.Commands
{
    public class DeleteRoleCommand : IRequest<CommandResult<bool>>
    {
        public string Id { get; set; }
    }
}
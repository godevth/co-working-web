using CoreCMS.Application.Role.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;
using System;

namespace CoreCMS.Application.Role.Queries
{
    public class GetRoleQuery : IRequest<CommandResult<RoleView>>
    {
        public string Id { get; set; }
    }
}

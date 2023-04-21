using System;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.UserType.Commands
{
    public class DeleteUserTypeCommand : IRequest<CommandResult<bool>>
    {
        public int Id { get; set; }
    }
}
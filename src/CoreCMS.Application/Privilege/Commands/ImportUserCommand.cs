using System.Collections.Generic;
using CoreCMS.Application.Privilege.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Privilege.Commands
{
    public class ImportUserCommand : IRequest<CommandResult<string>>
    {
        public string PlaceId { get; set;}
        public string CreateUserId { get; set; }
        public List<UserForm> ListUser { get; set;}
    }
}
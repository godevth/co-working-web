using CoreCMS.Application.UserType.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;
using System;

namespace CoreCMS.Application.UserType.Queries
{
    public class GetUserTypeQuery : IRequest<CommandResult<UserTypeView>>
    {
        public int Id { get; set; }
    }
}

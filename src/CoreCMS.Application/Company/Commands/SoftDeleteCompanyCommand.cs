using System;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Company.Commands
{
    public class SoftDeleteCompanyCommand : IRequest<CommandResult<bool>>
    {
        public int Id { get; set; }
        public string DeleteUserId { get; set; }
    }
}
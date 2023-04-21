using System;
using CoreCMS.Application.Company.Models;
using CoreCMS.Application.RoomType.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Company.Commands
{
    public class EditCompanyCommand : CompanyModels, IRequest<CommandResult<bool>>
    {
        public string UpdateUserId { get; set; }
    }
}
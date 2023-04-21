using CoreCMS.Application.Company.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Company.Commands
{
    public class AddCompanyCommand : CompanyModels, IRequest<CommandResult<bool>>
    {
        public string CreateUserId { get; set; }
    }
}
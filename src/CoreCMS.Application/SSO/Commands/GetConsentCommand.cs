using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.SSO.Models;
using MediatR;

namespace CoreCMS.Application.SSO.Commands
{
    public class GetConsentCommand  : IRequest<CommandResult<GetConsentResponse>>
    {
        public string UserId { get; set; }
    }
}
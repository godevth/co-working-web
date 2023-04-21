using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.SSO.Models;
using MediatR;

namespace CoreCMS.Application.SSO.Commands
{
    public class SubmitConsentCommand : IRequest<CommandResult<SubmitConsentResponse>>
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
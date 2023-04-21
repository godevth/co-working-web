using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.SSO.Models;
using MediatR;

namespace CoreCMS.Application.SSO.Commands
{
    public class RequestTokenCommand : RequestTokenParam, IRequest<CommandResult<RequestTokenResponse>>
    {
        
    }
}
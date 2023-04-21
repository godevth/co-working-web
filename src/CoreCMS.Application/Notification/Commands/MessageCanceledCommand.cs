using System.Collections.Generic;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Notification.Commands
{
    public class MessageCanceledCommand : IRequest<CommandResult<List<string>>>
    {
        public string AppId { get; set; }
        public string RefCode { get; set; }
        public string NotificationType { get; set; }
        
    }
}
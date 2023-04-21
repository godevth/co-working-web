using System;
using System.Collections.Generic;
using CoreCMS.Application.Notification.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Notification.Commands
{
    public class MessageCommand : NotificationForm,IRequest<CommandResult<List<string>>>
    {
        public string CreatedUserId;
    }
}
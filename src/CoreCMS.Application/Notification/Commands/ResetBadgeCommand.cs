using CoreCMS.Application.Notification.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Notification.Commands
{
   public class ResetBadgeCommand: BageForm, IRequest<CommandResult<bool>>
    {

    }
}

using CoreCMS.Application.Notification.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Notification.Commands
{
    public class AddDeviceCommand : DeviceForm, IRequest<CommandResult<string>>
    {
    }
}
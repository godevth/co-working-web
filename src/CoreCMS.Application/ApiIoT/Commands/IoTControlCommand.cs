using CoreCMS.Application.ApiIoT.Models;
using MediatR;

namespace CoreCMS.Application.ApiIoT.Commands
{
    public class IoTControlCommand : IoTControlForm, IRequest<bool>
    {
    }
}
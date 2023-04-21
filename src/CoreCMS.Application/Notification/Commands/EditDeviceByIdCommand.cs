using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.Device.Commands
{
    public class EditDeviceByIdCommand : IRequest<CommandResult<string>>
    {
        public string Token { get; set; }
        public string Language { get; set; }
    }
}
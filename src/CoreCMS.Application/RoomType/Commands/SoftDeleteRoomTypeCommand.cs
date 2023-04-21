using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.RoomType.Commands
{
    public class SoftDeleteRoomTypeCommand : IRequest<CommandResult<bool>>
    {
        public int Id { get; set; }
        public string DeleteUserId { get; set; }
    }
}
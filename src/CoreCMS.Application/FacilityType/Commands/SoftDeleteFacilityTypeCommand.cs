using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.FacilityType.Commands
{
    public class SoftDeleteFacilityTypeCommand : IRequest<CommandResult<bool>>
    {
        public int Id { get; set; }
        public string DeleteUserId { get; set; }
    }
}
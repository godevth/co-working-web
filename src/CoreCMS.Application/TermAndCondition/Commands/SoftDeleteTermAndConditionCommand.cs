using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.TermAndCondition.Commands
{
    public class SoftDeleteTermAndConditionCommand : IRequest<CommandResult<bool>>
    {
        public int TermId { get; set; }
        public string DeleteUserId { get; set; }
    }
}
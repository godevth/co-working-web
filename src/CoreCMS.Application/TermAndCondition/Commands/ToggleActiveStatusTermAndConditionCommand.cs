using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.TermAndCondition.Commands
{
    public class ToggleActiveStatusTermAndConditionCommand : IRequest<CommandResult<bool>>
    {
        public int TermId { get; set; }
        public string UpdatedUserId { get; set; }
    }
}
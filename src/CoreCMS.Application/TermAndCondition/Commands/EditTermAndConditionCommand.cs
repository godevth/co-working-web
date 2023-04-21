using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.TermAndCondition.Models;
using MediatR;

namespace CoreCMS.Application.TermAndCondition.Commands
{
    public class EditTermAndConditionCommand : TermAndConditionForm, IRequest<CommandResult<string>>
    {
        public int TermId { get; set; }
        public string CurrentUserId { get; set; }
    }
}
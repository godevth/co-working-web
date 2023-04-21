using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.TermAndCondition.Models;
using MediatR;

namespace CoreCMS.Application.TermAndCondition.Commands
{
    public class AddTermAndConditionCommand : TermAndConditionForm, IRequest<CommandResult<string>>
    {
        public string CreateUserId { get; set; }
    }
}
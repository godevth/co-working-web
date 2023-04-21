using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.TermAndCondition.Commands
{
    public class SubmitPlaceConsentCommand  : IRequest<CommandResult<bool>>
    {
        public int TermId { get; set; }
        public string CurrentUserId { get; set; }
        public string CurrentEmail { get; set; }
    }
}
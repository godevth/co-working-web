using CoreCMS.Application.TermAndCondition.Models;
using MediatR;

namespace CoreCMS.Application.TermAndCondition.Queries
{
    public class GetTermAndConditionQuery : IRequest<TermAndConditionView>
    {
        public int TermId { get; set; }
    }
}
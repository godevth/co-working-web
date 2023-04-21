using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;

namespace CoreCMS.Application.TermAndCondition.Commands
{
    public class EditTermAndConditionCommandHandler : BaseDbContextHandler<EditTermAndConditionCommand, CommandResult<string>>
    {
        public EditTermAndConditionCommandHandler(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<CommandResult<string>> Handle(EditTermAndConditionCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถแก้ไขข้อกำหนดและเงื่อนไขการใช้งานได้"
            };

            var term = _context.TermAndCondition.Where(o => o.TermId == request.TermId).Single();
            term.Name = request.Name;
            term.TermTH = request.TermTH;
            term.TermEN = request.TermEN;
            term.UpdatedUserId = request.CurrentUserId;
            term.UpdatedDate = DateTime.Now;

            _context.Update(term);
            var res = await _context.SaveChangesAsync();

            cmdResult.Status = res > 0;
            if (cmdResult.Status)
            {
                cmdResult.Data = term.TermId.ToString();
                cmdResult.Message = "แก้ไขข้อกำหนดและเงื่อนไขการใช้งานสำเร็จ";
            }
            return await Task.FromResult(cmdResult);
        }
    }
}
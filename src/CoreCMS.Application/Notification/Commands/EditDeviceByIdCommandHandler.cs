using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Device.Commands
{
    public class EditDeviceByIdCommandHandler : BaseDbContextWithMediatorHandler<EditDeviceByIdCommand, CommandResult<string>>
    {
        public EditDeviceByIdCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<CommandResult<string>> Handle(EditDeviceByIdCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = null;
            cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถแก้ไขข้อมูล Device ได้"
            };

            try
            {
                if (!Language.AllLanguage.Contains(request.Language.Trim()))
                    throw new Exception($"ค่าของ Language ไม่ถูกต้อง (ใช้ '{Language.TH}' or '{Language.EN}' เท่านั้น)");
                var device = _context.Devices.Where(o => o.Token == request.Token &&!o.InActiveStatus).Single();
                device.Language = request.Language;

                _context.Update(device);
                var res = await _context.SaveChangesAsync();

                cmdResult.Status = res > 0;
                if(cmdResult.Status)
                {
                    cmdResult.Data = device.Id.ToString();
                    cmdResult.Message = "แก้ไขข้อมูล Device สำเร็จ";
                }
            }
            catch(Exception e)
            {
                cmdResult.Message = e.Message;
            }
            return await Task.FromResult(cmdResult);
        }
    }
}
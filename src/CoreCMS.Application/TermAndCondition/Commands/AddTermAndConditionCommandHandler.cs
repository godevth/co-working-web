using System;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Place.Queries;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.TermAndCondition.Commands
{
    public class AddTermAndConditionCommandHandler : BaseDbContextWithMediatorHandler<AddTermAndConditionCommand, CommandResult<string>>
    {
        public AddTermAndConditionCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public override async Task<CommandResult<string>> Handle(AddTermAndConditionCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถบันทึกข้อกำหนดและเงื่อนไขการใช้งานได้"
            };
            try
            {
                GetPlaceNameQuery placeNameQuery = new GetPlaceNameQuery()
                {
                    Id = request.PlaceId
                };
                var place = await _mediator.Send(placeNameQuery);
                if(place == null)
                    throw new Exception("ไม่พบข้อมูลสถานที่");

                Domain.TermAndCondition term = new Domain.TermAndCondition()
                {
                    Name = request.Name,
                    PlaceId = place.PlaceId.Value,
                    TermTH = request.TermTH,
                    TermEN = request.TermEN,
                    InActiveStatus = true,
                    CreatedUserId = request.CreateUserId,
                    CreatedDate = DateTime.Now,
                };
                _context.Add(term);
                var res = await _context.SaveChangesAsync();
                cmdResult.Status = res > 0;
                if(cmdResult.Status)
                {
                    cmdResult.Data = term.TermId.ToString();
                    cmdResult.Message = "บันทึกข้อกำหนดและเงื่อนไขการใช้งานสำเร็จ";
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
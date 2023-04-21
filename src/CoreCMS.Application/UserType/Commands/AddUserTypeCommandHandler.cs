using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Picture.Commands;
using CoreCMS.Application.UserType.Commands;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoreCMS.Application.UserType.Commands
{
    public class AddUserTypeCommandHandler : BaseDbContextHandler<AddUserTypeCommand, CommandResult<bool>>
    {
        protected IMediator _mediator;
        public AddUserTypeCommandHandler(ApplicationDbContext context,IMediator mediator) : base(context)
        {
            _mediator = mediator;
        }
        public override async Task<CommandResult<bool>> Handle(AddUserTypeCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถเพิ่มสิทธิ์ผู้ใช้งานได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try {
                   if(_context.UserType.Where(o=>(o.Name == request.Name)).Any()){
                       cmdResult.Message = "ไม่สามารถเพิ่มสิทธิ์ผู้ใช้งานได้ เนื่องจากมีสิทธิ์ผู้ใช้งานนี้แล้ว";
                       cmdResult.Data = false;
                     return await Task.FromResult<CommandResult<bool>>(cmdResult);
                   }
                    Domain.UserType data = new Domain.UserType()
                    {
                        Name = request.Name,
                        NormalizedName = request.Name.ToUpper(),
                        Description = request.Description,
                        InActiveStatus = request.InActiveStatus
                    };
                    
                    _context.Add(data);
                    var res = await _context.SaveChangesAsync();

                    
                    cmdResult.Status = res > 0;
                    if(cmdResult.Status)
                    {
                        cmdResult.Data = true;
                        cmdResult.Message = "เพิ่มสิทธิ์ผู้ใช้งานสำเร็จ";
                    }
                    trx.Commit ();
                }
                catch (Exception e) {
                    trx.Rollback ();
                    cmdResult.Message = e.Message;
                }
            }
          return await Task.FromResult<CommandResult<bool>>(cmdResult);
        }
    }
}
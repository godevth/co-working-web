using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.ApiMasterFile.Commands;
using CoreCMS.Application.Picture.Commands;
using CoreCMS.Application.Role.Commands;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoreCMS.Application.Role.Commands
{
    public class EditRoleCommandHandler : BaseDbContextHandler<EditRoleCommand, CommandResult<bool>>
    {
        protected IMediator _mediator;
        public EditRoleCommandHandler(ApplicationDbContext context,IMediator mediator) : base(context)
        {
            _mediator = mediator;
        }
        public override async Task<CommandResult<bool>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถแก้ไขสิทธิ์ผู้ใช้งานได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try {
                     if(_context.Roles.Where(o=>(o.Name == request.Name && o.UserTypeId == request.UserTypeId)&& o.Id != request.Id).Any()){
                       cmdResult.Message = "ไม่สามารถแก้ไขสิทธิ์ผู้ใช้งานได้ เนื่องจากมีสิทธิ์ผู้ใช้งานนี้แล้ว";
                       cmdResult.Data = false;
                     return await Task.FromResult<CommandResult<bool>>(cmdResult);
                   }
                    var getRole = _context.Roles.Where(o=>o.Id == request.Id).Single();
                    getRole.Name = request.Name;
                    getRole.Description = request.Description;
                    getRole.NormalizedName = request.Name.ToUpper();
                    getRole.UserTypeId = request.UserTypeId;

                    _context.Update(getRole);
                    var res = await _context.SaveChangesAsync();


                    cmdResult.Status = res > 0;
                    if(cmdResult.Status)
                    {
                        cmdResult.Data = true;
                        cmdResult.Message = "แก้ไขสิทธิ์ผู้ใช้งานสำเร็จ";
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
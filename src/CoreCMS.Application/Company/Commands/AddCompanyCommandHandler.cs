using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Picture.Commands;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoreCMS.Application.Company.Commands
{
    public class AddCompanyCommandHandler : BaseDbContextHandler<AddCompanyCommand, CommandResult<bool>>
    {
        protected IMediator _mediator;
        public AddCompanyCommandHandler(ApplicationDbContext context,IMediator mediator) : base(context)
        {
            _mediator = mediator;
        }
        public override async Task<CommandResult<bool>> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถสร้างข้อมูลบริษัทได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync ()) {
                try {
                   if(_context.Company.Where(o=>(o.CompanyName_TH==request.Name_TH||o.CompanyName_EN==request.Name_EN)&&!o.IsDeleted).Any()){
                       cmdResult.Message = "ไม่สามารถสร้างข้อมูลบริษัทได้ เนื่องจากมีชื่อบริษัทนี้แล้ว";
                       cmdResult.Data = false;
                     return await Task.FromResult<CommandResult<bool>>(cmdResult);
                   }
                    Domain.Company com = new Domain.Company()
                    {
                        CompanyName_TH = request.Name_TH,
                        CompanyName_EN = request.Name_EN,
                        OwnerId = request.OwnerId,
                        CreatedUserId = request.CreateUserId,
                        CreatedDate = DateTime.Now
                    };
                    _context.Add(com);
                    var res = await _context.SaveChangesAsync();

                    if (res == 0)
                        return await Task.FromResult(cmdResult);
                    
                    cmdResult.Status = res > 0;
                    if(cmdResult.Status)
                    {
                        cmdResult.Data = true;
                        cmdResult.Message = "สร้างข้อมูลบริษัทสำเร็จ";
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
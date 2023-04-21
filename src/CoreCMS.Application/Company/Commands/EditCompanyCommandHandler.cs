using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.ApiMasterFile.Commands;
using CoreCMS.Application.Picture.Commands;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;

namespace CoreCMS.Application.Company.Commands
{
    public class EditCompanyCommandHandler : BaseDbContextHandler<EditCompanyCommand, CommandResult<bool>>
    {
        protected IMediator _mediator;
        public EditCompanyCommandHandler(ApplicationDbContext context, IMediator mediator) : base(context)
        {
            _mediator = mediator;
        }
        public override async Task<CommandResult<bool>> Handle(EditCompanyCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = null;
            cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถแก้ไขข้อมูลบริษัทได้"
            };
            using (var trx = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (_context.Company.Where(o => (o.CompanyName_TH == request.Name_TH || o.CompanyName_EN == request.Name_EN) && o.CompanyId != Convert.ToInt32(request.Id)&&!o.IsDeleted).Any())
                    {
                        cmdResult.Message = "ไม่สามารถสร้างข้อมูลบริษัทได้ เนื่องจากมีชื่อบริษัทนี้แล้ว";
                        cmdResult.Data = false;
                        return await Task.FromResult<CommandResult<bool>>(cmdResult);
                    }
                    var getCompany = _context.Company.Where(o => o.CompanyId == request.Id).Single();
                    if (request.IsEdit)
                    {
                        getCompany.CompanyName_TH = request.Name_TH;
                        getCompany.CompanyName_EN = request.Name_EN;
                        getCompany.OwnerId = request.OwnerId;
                    }
                    getCompany.UpdatedUserId = request.UpdateUserId;
                    getCompany.UpdatedDate = DateTime.Now;

                    if (request.Profile != null && request.Profile.Count > 0)
                    {
                        List<CompanyProfiles> listF = new List<CompanyProfiles>();
                        var getItem = _context.CompanyProfiles.Where(o => o.CompanyId == request.Id && !o.IsDeleted).ToList();
                        var setItems = request.Profile.Where(o => o.Id != null).Select(o => o.Id).ToList();
                        var add = request.Profile.Where(o => o.Id == null).ToList();
                        var del = getItem.Where(o => !setItems.Contains(o.CompanyProfilesId)).ToList();
                        var up = request.Profile.Where(o => o.Id != null).ToList();
                        foreach (var item in add)
                        {
                            listF.Add(new CompanyProfiles()
                            {
                                AdminId = item.AdminId,
                                PlaceId = new Guid(item.PlaceId),
                                CreatedUserId = request.UpdateUserId,
                                InActiveStatus = false,
                                CreatedDate = DateTime.Now
                            });
                        }

                        foreach (var item in up)
                        {
                            var imp = _context.CompanyProfiles.Where(o => o.CompanyProfilesId == item.Id).Single();
                            imp.PlaceId = new Guid(item.PlaceId);
                            imp.AdminId = item.AdminId;
                            imp.UpdatedUserId = request.UpdateUserId;
                            imp.UpdatedDate = DateTime.Now;
                            listF.Add(imp);

                        }
                        foreach (var item in del)
                        {
                            item.IsDeleted = true;
                            item.DeletedUserId = request.UpdateUserId;
                            item.DeletedDate = DateTime.Now;
                            listF.Add(item);
                        }
                        getCompany.CompanyProfiles = listF;
                    }
                    else
                    {
                        var allItem = _context.CompanyProfiles.Where(o => o.CompanyId == request.Id && !o.IsDeleted).ToList();
                        if (allItem.Count > 0)
                        {
                            foreach (var item in allItem)
                            {
                                item.IsDeleted = true;
                                item.DeletedUserId = request.UpdateUserId;
                                item.DeletedDate = DateTime.Now;
                            }
                            _context.UpdateRange(allItem);
                        }

                    }

                    _context.Update(getCompany);
                    var res = await _context.SaveChangesAsync();

                    cmdResult.Status = res > 0;
                    if (cmdResult.Status)
                    {
                        cmdResult.Data = true;
                        cmdResult.Message = "แก้ไขข้อมูลบริษัทสำเร็จ";
                    }
                    trx.Commit();
                }
                catch (Exception e)
                {
                    trx.Rollback();
                    cmdResult.Message = e.Message;
                }
            }
            return await Task.FromResult<CommandResult<bool>>(cmdResult);
        }
    }
}
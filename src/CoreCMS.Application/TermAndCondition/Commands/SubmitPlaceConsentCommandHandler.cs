using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.SSO.Commands;
using CoreCMS.Application.TermAndCondition.Queries;
using CoreCMS.Data;
using CoreCMS.Persistence;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CoreCMS.Application.TermAndCondition.Commands
{
    public class SubmitPlaceConsentCommandHandler : BaseDbContextWithMediatorHandler<SubmitPlaceConsentCommand, CommandResult<bool>>
    {
        private readonly IPersistedGrantStore _ipersistedGrantStore;
        private readonly ConsentConfig _config;

        public SubmitPlaceConsentCommandHandler(ApplicationDbContext context, IMediator mediator, IPersistedGrantStore ipersistedGrantStore, IOptions<ConsentConfig> config) : base(context, mediator)
        {
            _ipersistedGrantStore = ipersistedGrantStore;
            _config = config.Value;
        }

        public override async Task<CommandResult<bool>> Handle(SubmitPlaceConsentCommand request, CancellationToken cancellationToken)
        {
            CommandResult<bool> cmdResult = new CommandResult<bool>()
            {
                Message = "ไม่สามารถ Submit Consent ได้"
            };

            var nestedTrans = _context.Database.CurrentTransaction != null;
            using (var ts = !nestedTrans ? _context.Database.BeginTransaction() : null)
            {
                try
                {
                    #region Place Consent
                    if(request.TermId != 0)
                    {
                        GetPlaceConsentByUserIdQuery consentQuery = new GetPlaceConsentByUserIdQuery()
                        {
                            UserId = request.CurrentUserId,
                            Page = 1,
                            ItemsPerPage = 10,
                            Language = "TH",
                        };
                        var consents = await _mediator.Send(consentQuery);
                        if(consents == null || consents.Total == 0)
                        {
                            return await Task.FromResult(cmdResult);
                        }
                        
                        var term = consents.Items.Where(o => o.TermId == request.TermId).SingleOrDefault();
                        if(term == null)
                        {
                            return await Task.FromResult(cmdResult);
                        }

                        #region Insert PersistedGrant
                        var json = JsonConvert.SerializeObject(new {
                            PlaceId = term.PlaceId.Value,
                            UserId = request.CurrentUserId
                        });

                        var persisted =  new PersistedGrant()
                        {
                            ClientId = _config.ClientId,
                            CreationTime = DateTime.Now,
                            SubjectId = request.CurrentUserId,
                            Type = "user_consent_granted",
                            Key = Guid.NewGuid().ToString(),
                            Data = json
                        };

                        await _ipersistedGrantStore.StoreAsync(persisted);
                        #endregion

                        Domain.UserConsent userConsent = new Domain.UserConsent()
                        {
                            TermId = request.TermId,
                            UserId = request.CurrentUserId,
                            PlaceId = term.PlaceId.Value,
                            PersistedGrantsKey = persisted.Key,
                            CreatedUserId = request.CurrentUserId,
                            CreatedDate = persisted.CreationTime
                        };
                        _context.Add(userConsent);
                        
                        var res = await _context.SaveChangesAsync();
                        cmdResult.Status = res > 0;
                    }
                    #endregion
                    #region Default Consent (SSO)
                    else 
                    {
                        SubmitConsentCommand submitConsentCommand = new SubmitConsentCommand()
                        {
                            UserId = request.CurrentUserId,
                            Email = request.CurrentEmail
                        };
                        var submit = await _mediator.Send(submitConsentCommand);
                        cmdResult.Status = submit.Status;
                    }
                    #endregion

                    if(cmdResult.Status)
                    {
                        if (!nestedTrans)
                            ts.Commit();
                            
                        cmdResult.Data = true;
                        cmdResult.Message = "Submit Consent สำเร็จ";
                    }
                }
                catch (Exception e)
                {
                    if (!nestedTrans)
                        ts.Rollback();

                    cmdResult.Message = e.Message;
                }
            }
            
            return await Task.FromResult(cmdResult);
        }
    }
}
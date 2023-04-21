using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.SSO.Commands;
using CoreCMS.Application.SSO.Models;
using CoreCMS.Data;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using RestSharp;

namespace CoreCMS.Application.Notification.Commands
{
    public class DeleteDeviceCommandHandler : BaseDbContextWithMediatorHandler<DeleteDeviceCommand, CommandResult<string>>
    {
        private readonly ApiConfig _config;
        private readonly IMediator _mediator;
        public DeleteDeviceCommandHandler(ApplicationDbContext context, IMediator mediator, IOptions<ApiConfig> config) : base(context, mediator)
        {
            _config = config.Value;
            _mediator = mediator;
        }

        public override async Task<CommandResult<string>> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = null;
            cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถแก้ไขข้อมูล Device ได้"
            };
            using (var ts = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    var device = _context.Devices.Where(o => !o.InActiveStatus && o.Token == request.Token.Trim() && o.UserOwnerId == request.UserId).FirstOrDefault();
                    if (device != null)
                    {
                        string url = _config.UnRegistDeviceUrl;
                        var datetime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                        RestClient cli = new RestClient(url);
                        var token = _config.AccessTokenForNotification;
                        var method = Method.PUT;
                        RestRequest req = new RestRequest("", method);
                        //Header
                        req.AddHeader("Content-Type", "application/json");
                        req.AddHeader("Authorization", "Bearer " + token);

                        //body
                        req.AddParameter("id", device.ResponeDeviceId, ParameterType.QueryString);
                        req.AddParameter("appId", device.OS == "Android" ? _config.AppIdAndroid : _config.AppIdiOS, ParameterType.QueryString);

                        var result = cli.Execute(req);
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            device.InActiveStatus = true;
                        }
                        else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            throw new Exception(System.Net.HttpStatusCode.Unauthorized + "");
                        }
                        _context.Update(device);
                        ts.Commit();
                        var res = await _context.SaveChangesAsync();

                        cmdResult.Status = res > 0;
                        if (cmdResult.Status)
                        {
                            cmdResult.Data = device.Id.ToString();
                            cmdResult.Message = "ลบข้อมูล Device สำเร็จ";
                        }
                    }
                    else
                    {
                        cmdResult.Status = false;
                        cmdResult.Data = device.Id.ToString();
                        cmdResult.Message = "ลบข้อมูล Device สำเร็จ";
                    }


                }
                catch (Exception e)
                {
                    cmdResult.Message = e.Message;
                }
            }
            return await Task.FromResult(cmdResult);
        }
        private async Task<CommandResult<RequestTokenResponse>> RequestTokenSSO()
        {
            RequestTokenCommand requestTokenCommand = new RequestTokenCommand()
            {
                ClientId = _config.ClientId,
                ClientSecret = _config.Secret,
                GrantType = _config.GrantType,
                Scope = _config.Scope,
                AuthoritySSO = _config.AuthoritySSO,
                Authority = _config.Authority,
                AccessTokenForNotification = true
            };
            try
            {
                var res = await _mediator.Send(requestTokenCommand);
                if (res.Status && res.Data != null)
                {
                    _config.AccessTokenForNotification = res.Data.AccessToken;
                }
                return await Task.FromResult(res);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Notification.Models;
using CoreCMS.Application.Shared;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.Shared.Models;
using CoreCMS.Application.SSO.Commands;
using CoreCMS.Application.SSO.Models;
using CoreCMS.Data;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CoreCMS.Application.Notification.Commands
{
    public class AddDeviceCommandHandler : IRequestHandler<AddDeviceCommand, CommandResult<string>>
    {
        private readonly ApiConfig _config;
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AddDeviceCommandHandler> _logger;
        public AddDeviceCommandHandler(ApplicationDbContext context, IMediator mediator, IOptions<ApiConfig> config, ILogger<AddDeviceCommandHandler> logger)
        {
            _config = config.Value;
            _mediator = mediator;
            _context = context;
            _logger = logger;
        }

        public async Task<CommandResult<string>> Handle(AddDeviceCommand request, CancellationToken cancellationToken)
        {
            CommandResult<string> cmdResult = null;
            cmdResult = new CommandResult<string>()
            {
                Message = "ไม่สามารถสร้างข้อมูล Device ได้"
            };

            await RequestTokenSSO();

            using (var trx = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (!DeviceType.AllType.Contains(request.OS.Trim()))
                        throw new Exception($"ค่าของ Type ไม่ถูกต้อง (ใช้ '{DeviceType.Android}' or '{DeviceType.iOS}' เท่านั้น)");

                    if (!Language.AllLanguage.Contains(request.Language.Trim()))
                        throw new Exception($"ค่าของ Language ไม่ถูกต้อง (ใช้ '{Language.TH}' or '{Language.EN}' เท่านั้น)");

                    await RequestTokenSSO();

                    var deviceByTokens = _context.Devices.Where(o => !o.InActiveStatus && o.Token == request.Token.Trim() && o.UserOwnerId == request.UserId).ToList();
                    if (!deviceByTokens.Any())
                    {
                        string url = _config.RegisterDeviceUrl;
                        var datetime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                        RestClient cli = new RestClient(url);
                        var method = Method.POST;
                        var body = new
                        {
                            AppId = request.OS == "Android" ? _config.AppIdAndroid : _config.AppIdiOS,
                            AppName = request.AppName,
                            DeviceToken = request.Token,
                            DeviceType = request.OS,
                            IsActive = "1",
                            RegisTimeStemp = datetime
                        };
                        RestRequest req = new RestRequest("", method);
                        //Header

                        req.AddHeader("Content-Type", "application/json");
                        req.AddHeader("Authorization", "Bearer " + _config.AccessTokenForNotification);
                        //body
                        req.AddJsonBody(body);

                        var result = cli.Execute(req);
                        Response<string> response = null;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            if (!string.IsNullOrEmpty(result.Content))
                            {
                                response = JToken.Parse(result.Content).ToObject<Response<string>>();
                                Domain.Device device = new Domain.Device()
                                {
                                    Token = request.Token,
                                    OS = request.OS,
                                    AppId = request.OS == "Android" ? _config.AppIdAndroid : _config.AppIdiOS,
                                    Language = request.Language.ToUpper(),
                                    UserOwnerId = request.UserId,
                                    ResponeDeviceId = response.Result
                                };
                                _context.Add(device);
                                var res = await _context.SaveChangesAsync();

                                cmdResult.Status = res > 0;
                                if (cmdResult.Status)
                                {
                                    cmdResult.Data = device.Id.ToString();
                                    cmdResult.Message = "สร้างข้อมูล Device สำเร็จ";
                                    trx.Commit();
                                }
                            }

                            _logger.Log(LogLevel.Information, "AddDeviceCommandHandler: StatusCode is {@status} Body is {@body}, Response is {@response}", result.StatusCode, body, result.Content);
                        }
                        _logger.Log(LogLevel.Information, "AddDeviceCommandHandler: StatusCode is {@status} Body is {@body}, Response is {@response}", result.StatusCode, body, result.Content);
                    }
                    else
                    {
                        cmdResult.Status = false;
                        cmdResult.Message = "สร้างข้อมูล Device สำเร็จ";
                    }

                    #region check if new user push noti
                    var user = _context.Users.Where(x => x.Id == request.UserId && !x.EmailConfirmed).SingleOrDefault();
                    if (user != null)
                    {
                        MessageCommand message = new MessageCommand()
                        {
                            UserId = request.UserId,
                            CreatedUserId = request.UserId,
                            IsAllDevice = false,
                            NotificationLink = "",
                            NotificationType = "",
                            RefCode = "",
                            SendDateTime = DateTime.Now,
                            SendTimeStamp = DateTime.Now,
                        };
                        var listNoti = new List<MessageNotis>();
                        listNoti.Add(new MessageNotis()
                        {
                            Subject = "ยินดีต้อนรับสมาชิกใหม่",
                            Detail = string.Format("คุณ {0} \nเข้าสู่การเป็นสมาชิกใหม่ของ Co-Space \nกรุณายืนยันตัวตนผ่านทาง Email ที่ท่านสมัคร", user.DisplayName),
                            Language = "TH"
                        });
                        //listNoti.Add(new MessageNotis()
                        //{
                        //    Subject = "Booking",
                        //    Detail = "Save Booking Success",
                        //    Language = "EN"
                        //});
                        message.MessageNoti = listNoti;
                        var res = await _mediator.Send(message);
                    }

                    #endregion
                }
                catch (Exception e)
                {
                    _logger.Log(LogLevel.Information, "AddDeviceCommandHandler: Exception is {0}", e);
                    trx.Rollback();
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

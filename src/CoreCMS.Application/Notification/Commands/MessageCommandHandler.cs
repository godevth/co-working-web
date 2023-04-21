using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreCMS.Application.Shared.Model;
using CoreCMS.Application.Shared.Models;
using CoreCMS.Application.SSO.Commands;
using CoreCMS.Application.SSO.Models;
using CoreCMS.Data;
using CoreCMS.Domain;
using CoreCMS.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;
using CoreCMS.Domain;
using Microsoft.Extensions.Logging;

namespace CoreCMS.Application.Notification.Commands.MessageCommandHandler
{
    public class MessageCommandHandler : IRequestHandler<MessageCommand, CommandResult<List<string>>>
    {
        private readonly ApiConfig _config;
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MessageCommandHandler> _logger;

        public MessageCommandHandler(ApplicationDbContext context, IOptions<ApiConfig> config, IMediator mediator, ILogger<MessageCommandHandler> logger)
        {
            _config = config.Value;
            _mediator = mediator;
            _context = context;
            _logger = logger;
        }

        public async Task<CommandResult<List<string>>> Handle(MessageCommand request, CancellationToken cancellationToken)
        {
            CommandResult<List<string>> cmdResult = new CommandResult<List<string>>()
            {
                Message = "ไม่สามารถเรียก API Message ได้"
            };
            Response<List<string>> response = null;

            //Request Token From SSO
            await RequestTokenSSO();

            // var nestedTrans = _context.Database.CurrentTransaction != null;
            // using (var trx = !nestedTrans ? _context.Database.BeginTransaction() : null)
            // {
            try
            {

                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var Noti = new Domain.Notification()
                {
                    CreatedUserId = request.CreatedUserId,
                    CreatedDate = DateTime.Now,
                    NotiCode = request.RefCode,
                    InActiveStatus = false,
                    NotiTypeCode = request.NotificationType,
                    UserOwnerId = request.UserId,
                };
                _context.Add(Noti);
                _context.SaveChanges();
                if (request.MessageNoti != null && request.MessageNoti.Count > 0)
                {
                    foreach (var item in request.MessageNoti)
                    {
                        var msg = new MessageNotification()
                        {
                            Details = item.Detail,
                            Language = item.Language,
                            Subject = item.Subject,
                            NotificationId = Noti.Id
                        };
                        _context.Add(msg);
                    }

                }

                var url = _config.MessageUrl;
                RestClient cli = new RestClient(url);

                var method = Method.POST;
                RestRequest req = new RestRequest("", method);

                //Header
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + _config.AccessTokenForNotification);

                //Body
                var sendUnixTimestamp = new DateTimeOffset(request.SendDateTime).ToUnixTimeMilliseconds();
                req.RequestFormat = DataFormat.Json;
                var queryDe = _context.Devices.Where(o => !o.InActiveStatus);

                if (!request.IsAllDevice && request.UserId != null)
                {
                    queryDe = queryDe.Where(o => o.UserOwnerId == request.UserId);
                }

                var device = queryDe.ToList();
                List<object> objs = new List<object>();
                if (device != null && device.Count > 0)
                {
                    foreach (var item in device)
                    {

                        var badgeCount = 0;
                        var count = _context.Badges.Where(o => o.DeviceId == item.Id).SingleOrDefault();
                        //อัพเดท bage
                        if (count == null)
                        {
                            Badge badge = new Badge()
                            {
                                DeviceId = item.Id,
                                UserId = request.UserId,
                                Count = 1
                            };
                            badgeCount = 1;
                            _context.Add(badge);
                        }
                        else
                        {
                            count.Count = count.Count + 1;
                            badgeCount = count.Count + 1;
                            _context.Update(count);
                        }

                        if (item.OS == DeviceType.iOS)
                        {
                            var obj = new
                            {
                                AppId = _config.AppIdiOS,
                                Payload = new
                                {
                                    Notification = new
                                    {
                                        aps = new
                                        {
                                            alert = new
                                            {
                                                title = request.MessageNoti.Where(o => o.Language == item.Language).Select(o => o.Subject).FirstOrDefault(),
                                                body = request.MessageNoti.Where(o => o.Language == item.Language).Select(o => o.Detail).FirstOrDefault(),
                                                notificationType = request.NotificationType,
                                                notificationLink = request.NotificationLink
                                            },
                                            badge = badgeCount,
                                            sound = "default"
                                        }
                                    }
                                },
                                IsAllDevice = request.IsAllDevice,
                                DeviceTokenList = new List<string>() { item.Token }.ToArray(),
                                DeviceType = item.OS,
                                SendTimeStemp = sendUnixTimestamp
                            };
                            objs.Add(obj);

                        }
                        else
                        {
                            if (request.IsAllDevice)
                            {
                                var obj = new
                                {
                                    AppId = _config.AppIdAndroid,
                                    Payload = new
                                    {
                                        Notification = new
                                        {
                                            title = request.MessageNoti.Where(o => o.Language == item.Language).Select(o => o.Subject).FirstOrDefault(),
                                            body = request.MessageNoti.Where(o => o.Language == item.Language).Select(o => o.Detail).FirstOrDefault(),
                                        },
                                        Data = new
                                        {
                                            notificationType = request.NotificationType,
                                            notificationLink = request.NotificationLink
                                        }
                                    },
                                    IsAllDevice = request.IsAllDevice,
                                    DeviceTokenList = new List<string>().ToArray(),
                                    DeviceType = item.OS,
                                    SendTimeStemp = sendUnixTimestamp
                                };
                                objs.Add(obj);
                            }
                            else
                            {
                                var obj = new
                                {
                                    AppId = _config.AppIdAndroid,
                                    Payload = new
                                    {
                                        Notification = new
                                        {
                                            title = request.MessageNoti.Where(o => o.Language == item.Language).Select(o => o.Subject).FirstOrDefault(),
                                            body = request.MessageNoti.Where(o => o.Language == item.Language).Select(o => o.Detail).FirstOrDefault(),
                                        },
                                        Data = new
                                        {
                                            notificationType = request.NotificationType,
                                            notificationLink = request.NotificationLink
                                        }
                                    },
                                    IsAllDevice = request.IsAllDevice,
                                    DeviceTokenList = new List<string>() { item.Token }.ToArray(),
                                    DeviceType = item.OS,
                                    SendTimeStemp = sendUnixTimestamp
                                };
                                objs.Add(obj);
                            }

                        }
                    }
                    req.AddJsonBody(objs);

                    var result = cli.Execute(req);
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        _logger.Log(LogLevel.Information, "MessageCommandHandler: StatusCode is {@status} Body is {@body}, Response is {@response}", result.StatusCode, objs, result.Content);

                        if (!string.IsNullOrEmpty(result.Content))
                        {
                            response = JToken.Parse(result.Content).ToObject<Response<List<string>>>();
                        }
                        if (response.Result.Count > 0)
                        {
                            foreach (var itmes in response.Result)
                            {
                                foreach (var de in device)
                                {
                                    var res = new LogNotification()
                                    {
                                        DeviceId = de.Id,
                                        NotificationId = Noti.Id,
                                        ResponeJobId = itmes,
                                        SetTimeSend = sendUnixTimestamp,
                                        Status = true
                                    };
                                    _context.Add(res);
                                }
                            }
                        }
                        cmdResult.Message = "เรียก API Message สำเร็จ";
                        cmdResult.Data = response.Result;
                        cmdResult.Status = true;
                    }
                    else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        _logger.Log(LogLevel.Information, "MessageCommandHandler: StatusCode is {@status} Body is {@body}, Response is {@response}", result.StatusCode, objs, result.Content);
                        await RequestTokenSSO();
                        await _mediator.Send(request);
                    }
                    else
                    {
                        _logger.Log(LogLevel.Information, "MessageCommandHandler: StatusCode is {@status} Body is {@body}, Response is {@response}", result.StatusCode, objs, result.Content);
                        if (!string.IsNullOrEmpty(result.Content))
                        {
                            throw new Exception(result.Content.ToString());
                        }
                    }
                    _context.SaveChanges();
                }
                else
                {
                    //throw new Exception("ไม่พบ Devince");
                }

                // trx.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(cmdResult);
            //}
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
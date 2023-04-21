using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCMS.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace CoreCMS.Application.Office365.Models
{
    public class MicrosoftGraphProvider : IGraphProvider
    {
        private readonly ILogger<MicrosoftGraphProvider> _logger;
        private readonly IGraphServiceClient _graphServiceClient;
        private readonly Office365Config _config;

        public MicrosoftGraphProvider(IGraphServiceClient graphServiceClient, ILogger<MicrosoftGraphProvider> logger, IOptions<Office365Config> config)
        {
            _graphServiceClient = graphServiceClient;
            _logger = logger;
            _config = config.Value;
        }

        public async Task<bool> SendEmail(string to, string subject, string body, bool html = true)
        {
            try
            {
                var listEmailTo = to.Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                var message = new Message
                {
                    Subject = subject,
                    Body = new ItemBody { Content = body, ContentType = html ? BodyType.Html : BodyType.Text },
                    //ToRecipients = new List<Recipient>()
                    //ToRecipients = new[] { new Recipient { EmailAddress = new EmailAddress { Address = _config.TestMode ? _config.TestEmail : to } } }
                };

                if (_config.TestMode)
                {
                    message.ToRecipients = new[] { new Recipient { EmailAddress = new EmailAddress { Address = _config.TestEmail } } };
                }
                else
                {
                    message.ToRecipients = listEmailTo.Select(x => new Recipient { EmailAddress = new EmailAddress { Address = x } }).ToList();
                }

                var request = _graphServiceClient.Me.SendMail(message, true);

                await request.Request().PostAsync();

                _logger.LogInformation("Email has been sent. The message is {@Message}", message);

                return true;
            }
            catch { }

            return false;
        }
    }
}
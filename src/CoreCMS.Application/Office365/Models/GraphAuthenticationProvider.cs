using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoreCMS.Data;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Newtonsoft.Json.Linq;

namespace CoreCMS.Application.Office365.Models
{
    public class GraphAuthenticationProvider : IAuthenticationProvider
    {
        private readonly Office365Config _config;
        public GraphAuthenticationProvider(IOptions<Office365Config> config)
        {
            _config = config.Value;
        }
        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            HttpClient client = new HttpClient();
            string tokenEndpoint = $"https://login.microsoftonline.com/{_config.TenantId}/oauth2/token";
            var body = $"resource={_config.GraphURI}&client_id={_config.ClientId}&client_secret={_config.ClientSecret}&grant_type=password&username={_config.ADUserName}&password={_config.ADPassword}&scope=user.read%20mail.send";
            var stringContent = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");

            var result = await client.PostAsync(tokenEndpoint, stringContent).ContinueWith<string>((response) =>
            {
                return response.Result.Content.ReadAsStringAsync().Result;
            });

            JObject jobject = JObject.Parse(result);

            var token = jobject["access_token"].Value<string>();
            request.Headers.Add("Authorization", "Bearer " + token);
        }
    }
}
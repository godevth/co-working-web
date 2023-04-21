namespace CoreCMS.Application.SSO.Models
{
    public class RequestTokenParam
    {
        public string Authority { get; set; }
        public string AuthoritySSO { get; set; }
        public bool AccessTokenForNotification { get; set; }
        public string Scope { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string GrantType { get; set; }
    }
}
namespace CoreCMS.Data
{
    public class ConsentConfig
    {
        public string Authority { get; set; }
        public string GrantType { get; set; }
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string Scope { get; set; }
        public string AccessToken { get; set; }
        public string ClientIdRequest { get; set; }
        public string ScopesRequest { get; set; }
        public string ResourceScopeName { get; set; }
        public string GetConsentUrl { get; set; }
        public string SubmitConsentUrl { get; set; }
    }
}
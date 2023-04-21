namespace CoreCMS.Data
{
    public class Office365Config
    {
        public string GraphURI { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ADUserName { get; set; }
        public string ADPassword { get; set; }
        public bool TestMode { get; set; }
        public string TestEmail { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Data
{
    public class OpenIDConfig
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ClientScopes { get; set; }
        public string HashSecret { get; set; }
    }
}

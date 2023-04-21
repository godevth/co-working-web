using System;

namespace CoreCMS.Data
{
    public class IdentityServerConfig
    {
        public string Authority { get; set; }

        public string Audience { get; set; }

        public bool RequireHttpsMetadata { get; set; }

        public string SigningCertPath { get; set; }
    }
}

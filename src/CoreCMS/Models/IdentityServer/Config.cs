using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCMS.Models.IdentityServer
{
    public class Config
    {
        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId (),
                new IdentityResources.Profile () // <-- usefull
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource> {
                new ApiResource ("corecms", "CoreCMS"),
                new ApiResource ("corecms.backoffice", "CoreCMS BackOffice"),
                new ApiResource ("corecms.mobile", "CoreCMS Mobile"),
                new ApiResource ("corecms.pos", "CoreCMS POS"),
                new ApiResource ("corecms.automate", "CoreCMS Automate")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client {
                    ClientId = "coworking.backoffice",
                    ClientName = "CoWorking BackOffice",
                    RequireConsent = false,

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,

                    RequireClientSecret = false,

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "corecms",
                        "corecms.backoffice"
                    }
                },
                new Client
                {
                    ClientId = "coworking.mobile",
                    ClientName = "CoWorking Mobile",
                    RequireConsent = false,

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,

                    ClientSecrets =
                    {
                        new Secret("$BPCoWorkingM0bile".Sha256())
                    },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "corecms",
                        "corecms.mobile"
                    }
                },
                new Client
                {
                    ClientId = "coworking.automate",
                    ClientName = "CoWorking Automate",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("COW@utomate".Sha256())
                    },

                    AllowedScopes = {
                        "corecms",
                        "corecms.automate"
                    }
                }
            };
        }
    }
}

using System.Collections.Generic;

namespace CoreCMS.Application.SSO.Models
{
    public class GetConsentResponse
    {
        public bool HasValidationError { get; set; }
        public string ValidationError { get; set; }
        public GetConsentResult Result { get; set; }
    }

    public class GetConsentResult
    {
        public string ClientName { get; set; }
        public string TermsAndConTH { get; set; }
        public string TermsAndConEN { get; set; }
        public bool AnotherProviderGranted { get; set; }
        public List<IdentityScope> IdentityScopes { get; set; }
        public List<ResourceScope> ResourceScopes { get; set; }
    }

    public class IdentityScope
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Emphasize { get; set; }
        public bool Required { get; set; }
        public bool Checked { get; set; }
        public List<Claim> Claims { get; set; }
    }

    public class ResourceScope
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Emphasize { get; set; }
        public bool Required { get; set; }
        public bool Checked { get; set; }
        public List<Claim> Claims { get; set; }
    }

    public class Claim
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string Display { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
    }
    
}
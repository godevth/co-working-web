namespace CoreCMS.Application.SSO.Models
{
    public class SubmitConsentResponse
    {
        public bool HasValidationError { get; set; }
        public string ValidationError { get; set; }
        public bool Status { get; set; }
    }
}
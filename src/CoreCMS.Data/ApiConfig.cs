using System.Collections.Generic;

namespace CoreCMS.Data
{
    public class ApiConfig
    {
        public string Authority { get; set; }
        public string GrantType { get; set; }
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string Scope { get; set; }
        public string SysName { get; set; }
        public string FolderId { get; set; }
        public string UploadFileUrl { get; set; }
        public List<string> AllowFileExtensions { get; set; }
        public string SubId { get; set; }
        public string DeleteFileUrl { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenForNotification { get; set; }
        public string ProvinceUrl { get; set; }
        public string AmphurUrl { get; set; }
        public string TumbolUrl { get; set; }
        public string RegisterDeviceUrl { get; set; }
        public string UnRegistDeviceUrl { get; set; }
        public string QRCodeGenerator { get; set; }
        public string MessageUrl { get; set; }
        public string MessageCanceledUrl { get; set; }
        public string AuthoritySSO { get; set; }
        public string AppIdiOS { get; set; }
        public string AppIdAndroid { get; set; }
        public string IoTControl { get; set; }
        
        public Dictionary<string,string> AllowFileExtensionsToDict 
        {
            //ContentType|ExtensionInApi
            get {
                if(AllowFileExtensions != null && AllowFileExtensions.Count > 0)
                {
                    Dictionary<string,string> allows = new Dictionary<string, string>();
                    foreach(string s in AllowFileExtensions)
                    {
                        string[] temp = s.Split("|");
                        if(temp.Length == 2)
                            allows.Add(temp[0], temp[1]);
                    }
                    return allows;                    
                }
                return null;
            }
        }
        
    }
}
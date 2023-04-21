using System.Threading.Tasks;

namespace CoreCMS.Application.Office365.Models
{
    public interface IGraphProvider
    {
         Task<bool> SendEmail(string to, string subject, string body, bool html = true);
    }
}
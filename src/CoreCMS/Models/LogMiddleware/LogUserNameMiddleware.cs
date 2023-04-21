using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Context;

namespace CoreCMS.Models.LogMiddleware
{
    public class LogUserNameMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDiagnosticContext _diagnosticContext;

        public LogUserNameMiddleware(RequestDelegate next, IDiagnosticContext diagnosticContext)
        {
            _next = next;
            _diagnosticContext = diagnosticContext;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                ClaimsIdentity iden = context.User.Identity as ClaimsIdentity;

                if (iden?.FindFirst("client_id") != null)
                {
                    _diagnosticContext.Set("ClientId", iden?.FindFirst(o => o.Type == "client_id").Value);
                }

                if (iden?.FindFirst("preferred_username") != null)
                {
                    _diagnosticContext.Set("UserName", iden?.FindFirst(o => o.Type == "preferred_username").Value);
                }
            }
            
            return _next(context);
        }
    }
}
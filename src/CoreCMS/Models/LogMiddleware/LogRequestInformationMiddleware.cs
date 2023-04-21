using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace CoreCMS.Models.LogMiddleware
{
    public class LogRequestInformationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDiagnosticContext _diagnosticContext;

        public LogRequestInformationMiddleware(RequestDelegate next, IDiagnosticContext diagnosticContext)
        {
            _next = next;
            _diagnosticContext = diagnosticContext;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.RouteValues.ContainsKey("area") && (
                context.Request.RouteValues["area"].ToString() == "Api" || 
                context.Request.RouteValues["area"].ToString() == "MApi" || 
                context.Request.RouteValues["area"].ToString() == "PosApi"
            ))
            {
                _diagnosticContext.Set("Query", context.Request.Query);
                _diagnosticContext.Set("RawBody", GetRequestRawBody(context.Request).Result);


                using (var swapStream = new MemoryStream())
                {
                    var originalResponseBody = context.Response.Body;
                    context.Response.Body = swapStream;

                    await _next(context);

                    if (context.Response.ContentType.Contains("application/json")
                        || context.Response.ContentType.Contains("text/json")
                        || context.Response.ContentType.Contains("text/plain"))
                    {
                        swapStream.Seek(0, SeekOrigin.Begin);
                        _diagnosticContext.Set("ResponseBody", new StreamReader(swapStream).ReadToEnd());
                    }

                    swapStream.Seek(0, SeekOrigin.Begin);

                    await swapStream.CopyToAsync(originalResponseBody);
                    context.Response.Body = originalResponseBody;
                }
            }
            else
            {
                await _next(context);
            }
        }

        private async Task<string> GetRequestRawBody(HttpRequest request) {
            var body = request.Body;

            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //...Then we copy the entire request stream into the new buffer.
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            //We convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            // Reset the request body stream position so the next middleware can read it
            request.Body.Position = 0;

            return bodyAsText;
        }

        private async Task<string> GetResponseRawBody(HttpResponse response) {
            var body = response.Body;

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            var buffer = new byte[Convert.ToInt32(response.ContentLength)];

            //...Then we copy the entire request stream into the new buffer.
            await response.Body.ReadAsync(buffer, 0, buffer.Length);

            //We convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            // Reset the request body stream position so the next middleware can read it
            response.Body.Position = 0;

            return bodyAsText;
        }
    }
}
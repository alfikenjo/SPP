using Ganss.XSS;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BO_SPP.Common
{
    public class AntiXssMiddleware
    {
        private readonly RequestDelegate _next;

        public AntiXssMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // enable buffering so that the request can be read by the model binders next
            httpContext.Request.EnableBuffering();

            // leaveOpen: true to leave the stream open after disposing,
            // so it can be read by the model binders
            using (var streamReader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                var raw = await streamReader.ReadToEndAsync();
                var sanitiser = new HtmlSanitizer();
                var sanitised = sanitiser.Sanitize(raw);
                raw = sanitised;

                //var streamWriter = new StreamWriter(httpContext.Request.Body, Encoding.UTF8, leaveOpen: true);
                //streamWriter.Write(raw);       

                //using (var streamWriter = new StreamWriter(httpContext.Request.Body, Encoding.UTF8, leaveOpen: true))
                //{
                //    await streamWriter.WriteAsync(raw);                   
                //}

                if (raw != sanitised)
                {
                    throw new BadHttpRequestException("XSS injection detected from middleware.");
                }
            }

            // rewind the stream for the next middleware
            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            await _next.Invoke(httpContext);
        }
    }
}

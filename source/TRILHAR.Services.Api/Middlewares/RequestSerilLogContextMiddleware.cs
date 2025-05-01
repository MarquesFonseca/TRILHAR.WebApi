using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Serilog.Context;
using System.Linq;
using System.Threading.Tasks;

namespace TRILHAR.Services.Api.Middlewares
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestSerilLogContextMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public RequestSerilLogContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            using (LogContext.PushProperty("UserName", context?.User?.Identity?.Name ?? "anônimo"))
            using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
            {
                return _next.Invoke(context);
            }
        }

        private string GetCorrelationId(HttpContext httpContext)
        {
            //httpContext.Request.Headers.TryGetValue("Cko-Correlation-Id", out StringValues correlationId);
            httpContext.Request.Headers.TryGetValue("X-Correlation-ID", out StringValues correlationId);
            return correlationId.FirstOrDefault() ?? httpContext.TraceIdentifier;
        }
    }
}
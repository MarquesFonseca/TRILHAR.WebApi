using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Serilog;
using System;
using System.Linq;

namespace TRILHAR.Services.Api.Extensions
{
    /// <summary>
    /// LogEnricherExtensions
    /// </summary>
    public static class LogEnricherExtensions
    {
        /// <summary>
        /// EnrichFromRequest
        /// </summary>
        /// <param name="diagnosticContext"></param>
        /// <param name="httpContext"></param>
        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            diagnosticContext.Set("UserName", httpContext?.User?.Identity?.Name);
            diagnosticContext.Set("ClientIP", httpContext?.Connection?.RemoteIpAddress?.ToString());
            diagnosticContext.Set("UserAgent", httpContext?.Request.Headers["User-Agent"].FirstOrDefault());
            diagnosticContext.Set("Resource", httpContext?.GetMetricsCurrentResourceName());
        }

        /// <summary>
        /// GetMetricsCurrentResourceName
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string? GetMetricsCurrentResourceName(this HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            Endpoint? endpoint = httpContext.Features.Get<IEndpointFeature>()?.Endpoint;

            return endpoint?.Metadata.GetMetadata<EndpointNameMetadata>()?.EndpointName;
        }
    }
}
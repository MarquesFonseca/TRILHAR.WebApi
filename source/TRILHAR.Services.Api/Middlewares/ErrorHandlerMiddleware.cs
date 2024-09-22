using ElmahCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TRILHAR.Services.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                await HandleExceptionAsync(context, error);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Log.Error(exception, "Erro não tratado");
            context.RiseError(exception);
            context.Response.Clear();
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(exception.Message);
            return context.Response.WriteAsync(result, Encoding.UTF8);
        }
    }
}

using ElmahCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TRILHAR.Services.Api.Attributes
{
    /// <summary>
    /// ActionFilterApiAttribute
    /// </summary>
    public class ActionFilterApiAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// OnException
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = 500;
            context.HttpContext.RiseError(context.Exception);

            base.OnException(context);
        }
    }
}
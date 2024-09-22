using ElmahCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TRILHAR.Services.Api.Attributes
{
    public class ActionFilterApiAttribute : ExceptionFilterAttribute
    {        
        public override void OnException(ExceptionContext context)
        {
            //if (context.Exception is TuxedoClientException)
            //{
            //    var result = new BadRequestObjectResult(context.Exception.Message);
            //    context.HttpContext.Response.StatusCode = 400;
            //    context.Result = result;
            //    return;
            //}
            //else
            //{
            //    context.HttpContext.Response.StatusCode = 500;
            //    context.HttpContext.RiseError(context.Exception);
            //}
            
            base.OnException(context);
        }
    }
}
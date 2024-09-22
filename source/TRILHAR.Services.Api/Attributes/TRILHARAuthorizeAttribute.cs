using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TRILHAR.Infra.Data;
using System.Security.Claims;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace TRILHAR.Services.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TRILHARAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public bool PermitirAcessoExterno { get; set; }
        public TRILHARAuthorizeAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            
            if (allowAnonymous)
            {
                return;
            }

            if (!PermitirAcessoExterno)
            {
                var acessoExternoClaim = context.HttpContext.User.FindFirstValue("acesso_externo");
                if (!string.IsNullOrEmpty(acessoExternoClaim))
                {
                    var ehAcessoExterno = Convert.ToBoolean(acessoExternoClaim);
                    if (ehAcessoExterno)
                    {
                        context.Result = new ForbidResult();
                        return;
                    }
                }
            }

            if (context.HttpContext.Response.StatusCode == 401)
            {
                return;
            }

            var roleClaim = context.HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrEmpty(roleClaim))
            {
                context.Result = new ForbidResult();
                return;
            }

            var roles = roleClaim.Split(",");

            IServiceProvider services = context.HttpContext.RequestServices;
            var aplicacao = services.GetService<IOptions<Aplicacao>>();

            var sistema = aplicacao != null ? $"{aplicacao.Value.Sistema}{aplicacao.Value.Ano}" : string.Empty;
            if (!roles.Contains(sistema))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
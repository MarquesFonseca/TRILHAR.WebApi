using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Notificacoes;
using TRILHAR.Services.Api.Attributes;
using System.Linq;

namespace TRILHAR.Services.Api.Controllers
{

    /// <summary>
    /// BaseApi
    /// </summary>
    [ActionFilterApi]
    [TRILHARAuthorize]
    public class BaseApiController : ControllerBase
    {
        private readonly INotificador _notificador;
       
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notificador"></param>
        protected BaseApiController(INotificador notificador)
        {
            _notificador = notificador;
        }

        /// <summary>
        /// OperacaoValida
        /// </summary>
        /// <returns></returns>
        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        /// <summary>
        /// CustomResponse
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ActionResult CustomResponse(object result = null, int? totalCount = null)
        {
            if (OperacaoValida())
            {
                if(result == null)
                {
                    return NotFound();
                }

                if (totalCount.HasValue)
                {
                    HttpContext.Response.Headers.Add("total-count", totalCount.Value.ToString());
                }

                return Ok(result);
            }
            return BadRequest(_notificador.ObterNotificacoes().Select(n => n.Mensagem));
        }

        /// <summary>
        /// CustomResponse
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        /// <summary>
        /// NotificarErroModelInvalida
        /// </summary>
        /// <param name="modelState"></param>
        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        /// <summary>
        /// NotificarErro
        /// </summary>
        /// <param name="mensagem"></param>
        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }
    }
}

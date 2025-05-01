using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Notificacoes;
using TRILHAR.Services.Api.Attributes;

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
        /// CreatedResponse
        /// </summary>
        /// <param name="result"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        protected ActionResult CreatedResponse(object result, object id)
        {
            var url = $"{Request.Scheme}://{Request.Host}{Request.Path}/{id}";

            return Created(url, result);
        }

        /// <summary>
        /// CustomResponse
        /// </summary>
        /// <param name="result"></param>
        /// <param name="isNotFound"></param>
        /// <returns></returns>
        protected ActionResult CustomResponse(object? result = null, bool isNotFound = false)
        {
            if (OperacaoValida())
            {
                return Ok(new { Dados = result });
            }

            var mensagens = _notificador.ObterNotificacoes().Select(n => n.Mensagem).ToList();
            var msgsString = string.Join("\n", mensagens);

            var problemDetails = new ProblemDetails
            {
                Title = isNotFound ? "Registro não encontrado" : "Erro de validação",
                Status = isNotFound ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest,
                Detail = msgsString,
                Instance = HttpContext?.Request?.Path
            };

            problemDetails.Extensions["erros"] = mensagens;

            return new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status
            };
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
        /// OperacaoValida
        /// </summary>
        /// <returns></returns>
        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
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

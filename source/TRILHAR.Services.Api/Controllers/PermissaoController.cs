using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.IO.Pagina;
using TRILHAR.Business.IO.Permissao;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Services.Api.Controllers
{
    /// <summary>
    /// Controller.
    /// Contém todos os métodos dessa funcionalidade.
    /// </summary>
    [ApiController]
    [Route("api/permissao")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class PermissaoController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PermissaoEntity> _logger;
        private readonly IPermissaoService _PermissaoService;
        private readonly IPermissaoRepository _PermissaoRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notificador"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="PermissaoService"></param>
        /// <param name="PermissaoRepository"></param>
        /// 
        public PermissaoController(
            INotificador notificador,
            IMapper mapper,
            ILogger<PermissaoEntity> logger,
            IPermissaoService PermissaoService,
            IPermissaoRepository PermissaoRepository
            ) : base(notificador)
        {
            _mapper = mapper;
            _logger = logger;
            _PermissaoService = PermissaoService;
            _PermissaoRepository = PermissaoRepository;
        }

        /// <summary>
        /// Retorna todos os Registro
        /// </summary>
        /// <returns>Retorna todos Permissaos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PermissaoOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get()
        {
            var resultado = await _PermissaoRepository.GetAllAsync();
            if (resultado == null || !resultado.Any())
            {
                _logger.LogWarning("Registro não encontrado.");
                NotificarErro("Registro não encontrado.");
                return CustomResponse(isNotFound: true);
            }
            var permissaoOutput = _mapper.Map<IEnumerable<PermissaoOutput>>(resultado);
            return CustomResponse(permissaoOutput);
        }

        ///// <summary>
        ///// Retorna todos por parametros e Permissaoção
        ///// </summary>
        ///// <returns>Retorna todos Permissaos</returns>
        //[HttpPost("ListarPorFiltro")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<PermissaoEntity>))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<IActionResult> ListarPorFiltro([FromBody] InputPaginado input)
        //{
        //    if (!ModelState.IsValid) return CustomResponse(ModelState);

        //    if (input == null)
        //    {
        //        return BadRequest("O filtro não pode ser nulo.");
        //    }

        //    if(input.IsPaginacao && input.Page == 0)
        //    {
        //        return BadRequest("O filtro 'Page 'não pode ser 0.");
        //    }

        //    if (input.IsPaginacao && input.PageSize == 0)
        //    {
        //        return BadRequest("O filtro 'PageSize 'não pode ser 0.");
        //    }

        //    var resultado = await _PermissaoRepository.GetByPaginacaoAsync(input);
        //    if (OperacaoValida())
        //    {
        //        return Ok(resultado);
        //    }
        //    return CustomResponse(resultado);
        //}

        /// <summary>
        /// Retorna o Registro por codigo
        /// </summary>
        /// <param name="id">Informe o id.</param>
        /// <returns>Retorna Permissao</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermissaoOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = await _PermissaoRepository.GetByCodigoAsync(id);
            if (resultado == null)
            {
                _logger.LogWarning("Permissão com ID {Id} não encontrado.", id);
                NotificarErro("Registro não encontrado.");
                return CustomResponse(isNotFound: true);
            }
            var permissaoOutput = _mapper.Map<PermissaoOutput>(resultado);
            return CustomResponse(permissaoOutput);
        }

        /// <summary>
        /// Incluir novo Registro
        /// </summary>
        /// <param name="registro">Informe o registro</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> CreatePermissaoAsync([FromBody] PermissaoInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            _logger.LogInformation("Criando nova permissão: {@Permissao}", registro);
            var resultado = await _PermissaoRepository.InsertAsync(registro);
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Alterar Registro
        /// </summary>
        /// <param name="id">Informe o id do registro</param>
        /// <param name="input">Informe o registro</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> UpdatePermissaoAsync(int id, [FromBody] PermissaoInput input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var reg = await _PermissaoRepository.GetByCodigoAsync(id);
            if (reg == null)
            {
                _logger.LogWarning("Tentativa de atualização para permissão ID {Id}, mas não encontrado.", id);
                NotificarErro("Registro não existe!");
                return CustomResponse();
            }

            input.DataCadastro = reg.DataCadastro;

            _logger.LogInformation("Atualizando permissão ID {Id}: {@Input}", id, input);
            var resultado = await _PermissaoRepository.UpdateAsync(input);
            return CustomResponse(resultado);
        }
    }
}

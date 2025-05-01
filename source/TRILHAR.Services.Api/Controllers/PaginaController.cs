using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.IO.Matricula;
using TRILHAR.Business.IO.Pagina;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Services.Api.Controllers
{
    /// <summary>
    /// Controller.
    /// Contém todos os métodos dessa funcionalidade.
    /// </summary>
    [ApiController]
    [Route("api/paginas")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class PaginaController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PaginaEntity> _logger;
        private readonly IPaginaService _PaginaService;
        private readonly IPaginaRepository _PaginaRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notificador"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="PaginaService"></param>
        /// <param name="PaginaRepository"></param>
        /// 
        public PaginaController(
            INotificador notificador,
            IMapper mapper,
            ILogger<PaginaEntity> logger,
            IPaginaService PaginaService,
            IPaginaRepository PaginaRepository
            ) : base(notificador)
        {
            _mapper = mapper;
            _logger = logger;
            _PaginaService = PaginaService;
            _PaginaRepository = PaginaRepository;
        }

        /// <summary>
        /// Retorna todos os Registro
        /// </summary>
        /// <returns>Retorna todos Paginas</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaginaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get()
        {
            var resultado = await _PaginaRepository.GetAllAsync();
            if (resultado == null || !resultado.Any())
            {
                NotificarErro("Registro não encontrado!");
                return CustomResponse(isNotFound: true);
            }
            var paginaOutput = _mapper.Map<IEnumerable<PaginaOutput>>(resultado);
            return CustomResponse(paginaOutput);
        }

        /// <summary>
        /// Retorna todos por parametros e paginação
        /// </summary>
        /// <returns>Retorna todos Paginas</returns>
        [HttpPost("ListarPorFiltro")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<PaginaEntity>))]
        public async Task<IActionResult> ListarPorFiltro(
            [FromBody] InputPaginado input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (input == null)
            {
                return BadRequest("O filtro não pode ser nulo.");
            }

            if(input.IsPaginacao && input.Page == 0)
            {
                return BadRequest("O filtro 'Page 'não pode ser 0.");
            }

            if (input.IsPaginacao && input.PageSize == 0)
            {
                return BadRequest("O filtro 'PageSize 'não pode ser 0.");
            }

            var resultado = await _PaginaRepository.GetByPaginacaoAsync(input);
            if (OperacaoValida())
            {
                return Ok(resultado);
            }
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna o Registro por codigo
        /// </summary>
        /// <param name="id">Informe o id.</param>
        /// <returns>Retorna Pagina</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = await _PaginaRepository.GetByCodigoAsync(id);
            if (resultado == null)
            {
                NotificarErro("Registro não encontrado.");
                return CustomResponse(isNotFound: true);
            }
            var paginaOutput = _mapper.Map<IEnumerable<PaginaOutput>>(resultado);
            return CustomResponse(paginaOutput);
        }

        /// <summary>
        /// Incluir novo Registro
        /// </summary>
        /// <param name="registro">Informe o registro</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Post([FromBody] PaginaInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _PaginaRepository.InsertAsync(registro);
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
        public async Task<IActionResult> Put(int id, [FromBody] PaginaInput input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var reg = await _PaginaRepository.GetByCodigoAsync(id);
            if (reg == null)
            {
                NotificarErro("Registro não existe!");
                return CustomResponse();
            }

            input.DataCadastro = reg.DataCadastro;
            var resultado = await _PaginaRepository.UpdateAsync(input);
            return CustomResponse(resultado);
        }
    }
}

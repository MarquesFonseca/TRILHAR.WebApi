using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.IO.Aluno;
using TRILHAR.Business.IO.Permissao;
using TRILHAR.Business.IO.Turma;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Services.Api.Controllers
{
    /// <summary>
    /// Controller.
    /// Contém todos os métodos dessa funcionalidade.
    /// </summary>
    [ApiController]
    [Route("api/turmas")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class TurmaController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TurmaEntity> _logger;
        private readonly ITurmaService _TurmaService;
        private readonly ITurmaRepository _TurmaRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notificador"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="TurmaService"></param>
        /// <param name="TurmaRepository"></param>
        /// 
        public TurmaController(
            INotificador notificador,
            IMapper mapper,
            ILogger<TurmaEntity> logger,
            ITurmaService TurmaService,
            ITurmaRepository TurmaRepository
            ) : base(notificador)
        {
            _mapper = mapper;
            _logger = logger;
            _TurmaService = TurmaService;
            _TurmaRepository = TurmaRepository;
        }

        /// <summary>
        /// Retorna todos os Registro
        /// </summary>
        /// <returns>Retorna todos Turmas</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TurmaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get()
        {
            var resultado = await _TurmaRepository.GetAllAsync();
            if (resultado == null || !resultado.Any())
            {
                _logger.LogWarning("Registro não encontrado.");
                NotificarErro("Registro não encontrado.");
                return CustomResponse(isNotFound: true);
            }
            var turmaOutput = _mapper.Map<IEnumerable<TurmaOutput>>(resultado);
            return CustomResponse(turmaOutput);
        }

        /// <summary>
        /// Retorna todas as turmas ativas
        /// </summary>
        /// <returns>Retorna todos Turmas</returns>
        [HttpGet("ListarTurmasAtivas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TurmaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> ListarTurmasAtivas()
        {
            var resultado = await _TurmaService.ListarTurmasAtivas();
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna todos por parametros e paginação
        /// </summary>
        /// <returns>Retorna todos Turmas</returns>
        [HttpPost("ListarPorFiltro")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<TurmaEntity>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> ListarPorFiltro([FromBody] InputPaginado input)
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

            var resultado = await _TurmaRepository.GetByPaginacaoAsync(input);

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
        /// <returns>Retorna Turma</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TurmaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = await _TurmaRepository.GetByCodigoAsync(id);
            if (resultado == null)
            {
                _logger.LogWarning("Turma com ID {Id} não encontrado.", id);
                NotificarErro("Registro não encontrado.");
                return CustomResponse(isNotFound: true);
            }
            var turmaOutput = _mapper.Map<TurmaOutput>(resultado);
            return CustomResponse(turmaOutput);
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
        public async Task<IActionResult> CreateTurmaAsync([FromBody] TurmaInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            _logger.LogInformation("Criando nova turma: {@Turma}", registro);
            var resultado = await _TurmaRepository.InsertOutputInsertedAsync(registro);
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
        public async Task<IActionResult> UpdateTurmaAsync(int id, [FromBody] TurmaInput input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var reg = await _TurmaRepository.GetByCodigoAsync(id);
            if (reg == null)
            {
                _logger.LogWarning("Tentativa de atualização para turma ID {Id}, mas não encontrado.", id);
                NotificarErro("Registro não existe!");
                return CustomResponse();
            }

            input.DataCadastro = reg.DataCadastro;

            _logger.LogInformation("Atualizando turma ID {Id}: {@Input}", id, input);
            var resultado = await _TurmaRepository.UpdateAsync(input);
            return CustomResponse(resultado);
        }
    }
}

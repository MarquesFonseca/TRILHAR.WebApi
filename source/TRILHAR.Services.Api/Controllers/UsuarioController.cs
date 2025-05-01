using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.IO.Turma;
using TRILHAR.Business.IO.Usuario;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Services.Api.Controllers
{
    /// <summary>
    /// Controller.
    /// Contém todos os métodos dessa funcionalidade.
    /// </summary>
    [ApiController]
    [Route("api/usuarios")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class UsuarioController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UsuarioEntity> _logger;
        private readonly IUsuarioService _UsuarioService;
        private readonly IUsuarioRepository _UsuarioRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notificador"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="UsuarioService"></param>
        /// <param name="UsuarioRepository"></param>
        public UsuarioController(
            INotificador notificador,
            IMapper mapper,
            ILogger<UsuarioEntity> logger,
            IUsuarioService UsuarioService,
            IUsuarioRepository UsuarioRepository
            ) : base(notificador)
        {
            _mapper = mapper;
            _logger = logger;
            _UsuarioService = UsuarioService;
            _UsuarioRepository = UsuarioRepository;
        }

        /// <summary>
        /// Retorna todos os Registro
        /// </summary>
        /// <returns>Retorna todos Usuarios</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UsuarioOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get()
        {
            var resultado = await _UsuarioRepository.GetAllAsync();
            if (resultado == null || !resultado.Any())
            {
                NotificarErro("Registro não encontrado!");
                return CustomResponse(isNotFound: true);
            }
            var usuarioOutput = _mapper.Map<IEnumerable<UsuarioOutput>>(resultado);
            return CustomResponse(usuarioOutput);
        }

        /// <summary>
        /// Retorna todos por parametros e paginação
        /// </summary>
        /// <returns>Retorna todos Usuarios</returns>
        [HttpPost("ListarPorFiltro")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<UsuarioEntity>))]
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

            var resultado = await _UsuarioRepository.GetByPaginacaoAsync(input);
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
        /// <returns>Retorna Usuario</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = await _UsuarioRepository.GetByCodigoAsync(id);
            if (resultado == null)
            {
                NotificarErro("Registro não encontrado.");
                return CustomResponse(isNotFound: true);
            }
            var usuarioOutput = _mapper.Map<UsuarioOutput>(resultado);
            return CustomResponse(usuarioOutput);
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
        public async Task<IActionResult> Post([FromBody] UsuarioInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _UsuarioRepository.InsertAsync(registro);
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
        public async Task<IActionResult> Put(int id, [FromBody] UsuarioInput input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var reg = await _UsuarioRepository.GetByCodigoAsync(id);
            if (reg == null)
            {
                NotificarErro("Registro não existe!");
                return CustomResponse();
            }

            input.DataCadastro = reg.DataCadastro;
            var resultado = await _UsuarioRepository.UpdateAsync(input);
            return CustomResponse(resultado);
        }
    }
}

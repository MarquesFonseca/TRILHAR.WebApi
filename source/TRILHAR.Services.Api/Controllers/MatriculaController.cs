using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.IO.Matricula;
using TRILHAR.Business.Pagination;
using TRILHAR.Infra.Data.Repositories;

namespace TRILHAR.Services.Api.Controllers
{
    /// <summary>
    /// Controller.
    /// Contém todos os métodos dessa funcionalidade.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class MatriculaController : BaseApiController
    {
        private readonly ILogger<MatriculaAlunoTurmaEntity> _logger;
        private readonly IMatriculaAlunoTurmaService _MatriculaService;
        private readonly IMatriculaAlunoTurmaRepository _MatriculaRepository;
        private readonly IVMatriculaAlunoTurmaRepository _VMatriculaAlunoTurmaRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notificador"></param>
        /// <param name="logger"></param>
        /// <param name="MatriculaService"></param>
        /// <param name="MatriculaRepository"></param>
        /// <param name="VMatriculaAlunoTurmaRepository"></param>
        /// 
        public MatriculaController(
            INotificador notificador,
            ILogger<MatriculaAlunoTurmaEntity> logger,
            IMatriculaAlunoTurmaService MatriculaService,
            IMatriculaAlunoTurmaRepository MatriculaRepository,
            IVMatriculaAlunoTurmaRepository VMatriculaAlunoTurmaRepository
            ) : base(notificador)
        {
            _logger = logger;
            _MatriculaService = MatriculaService;
            _MatriculaRepository = MatriculaRepository;
            _VMatriculaAlunoTurmaRepository = VMatriculaAlunoTurmaRepository;
        }

        /// <summary>
        /// Retorna todos os Registro
        /// </summary>
        /// <returns>Retorna todos Matriculas</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultado = await _MatriculaRepository.GetAllAsync();
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna todos por parametros e paginação
        /// </summary>
        /// <returns>Retorna todos Matriculas</returns>
        [HttpPost("ListarPorFiltro")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<MatriculaAlunoTurmaEntity>))]
        public async Task<IActionResult> ListarPorFiltro(
            [FromBody] InputPaginado input)
        {
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

            //var resultado = await _MatriculaRepository.GetByPaginacaoAsync(input);
            var resultado = await _VMatriculaAlunoTurmaRepository.GetByPaginacaoAsync(input);
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
        /// <returns>Retorna Matricula</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = await _MatriculaRepository.GetByCodigoAsync(id);
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Incluir novo Registro
        /// </summary>
        /// <param name="registro">Informe o registro</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MatriculaInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _MatriculaService.InsertAsync(registro);
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Alterar Registro
        /// </summary>
        /// <param name="registro">Informe o registro</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MatriculaInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var reg = await _MatriculaRepository.GetByCodigoAsync(registro.Codigo);

            if (reg == null)
            {
                NotificarErro("Registro não existe!");
                return CustomResponse();
            }

            var resultado = await _MatriculaService.UpdateAsync(registro);
            return CustomResponse(resultado);
        }
    }
}

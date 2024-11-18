using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO.Turma;
using TRILHAR.Business.IO.Paginacao;
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
    public class TurmaController : BaseApiController
    {
        private readonly ILogger<TurmaEntity> _logger;
        private readonly ITurmaService _TurmaService;
        private readonly ITurmaRepository _TurmaRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notificador"></param>
        /// <param name="logger"></param>
        /// <param name="TurmaService"></param>
        /// <param name="TurmaRepository"></param>
        /// 
        public TurmaController(
            INotificador notificador,
            ILogger<TurmaEntity> logger,
            ITurmaService TurmaService,
            ITurmaRepository TurmaRepository
            ) : base(notificador)
        {
            _logger = logger;
            _TurmaService = TurmaService;
            _TurmaRepository = TurmaRepository;
        }

        /// <summary>
        /// Retorna todos os Registro
        /// </summary>
        /// <returns>Retorna todos Turmas</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultado = await _TurmaRepository.GetAllAsync();
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna todos por parametros e paginação
        /// </summary>
        /// <returns>Retorna todos Turmas</returns>
        [HttpPost("ListarPorFiltro")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<TurmaEntity>))]
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
        public async Task<IActionResult> Get(int id)
        {
            var resultado = await _TurmaRepository.GetByCodigoAsync(id);
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Incluir novo Registro
        /// </summary>
        /// <param name="registro">Informe o registro</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TurmaInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            //var resultado = await _TurmaService.NovoRegistroAsync(registro);
            var resultado = new TurmaEntity();
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Alterar Registro
        /// </summary>
        /// <param name="registro">Informe o registro</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TurmaInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var reg = await _TurmaRepository.GetByCodigoAsync(registro.Codigo);

            if (reg == null)
            {
                NotificarErro("Registro não existe!");
                return CustomResponse();
            }

            //var resultado = await _TurmaService.AtualizarRegistroAsync(registro);
            var resultado = new TurmaEntity();
            return CustomResponse(resultado);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO.Frequencia;
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
    public class FrequenciaController : BaseApiController
    {
        private readonly ILogger<FrequenciaEntity> _logger;
        private readonly IFrequenciaService _FrequenciaService;
        private readonly IFrequenciaRepository _FrequenciaRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notificador"></param>
        /// <param name="logger"></param>
        /// <param name="FrequenciaService"></param>
        /// <param name="FrequenciaRepository"></param>
        /// 
        public FrequenciaController(
            INotificador notificador,
            ILogger<FrequenciaEntity> logger,
            IFrequenciaService FrequenciaService,
            IFrequenciaRepository FrequenciaRepository
            ) : base(notificador)
        {
            _logger = logger;
            _FrequenciaService = FrequenciaService;
            _FrequenciaRepository = FrequenciaRepository;
        }

        /// <summary>
        /// Retorna todos os Registro
        /// </summary>
        /// <returns>Retorna todos Frequencias</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultado = await _FrequenciaRepository.GetAllAsync();
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna todos por parametros e paginação
        /// </summary>
        /// <returns>Retorna todos Frequencias</returns>
        [HttpPost("ListarPorFiltro")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<FrequenciaEntity>))]
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

            var resultado = await _FrequenciaRepository.GetByPaginacaoAsync(input);
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
        /// <returns>Retorna Frequencia</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = await _FrequenciaRepository.GetByCodigoAsync(id);
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Incluir novo Registro
        /// </summary>
        /// <param name="registro">Informe o registro</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FrequenciaInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            //var resultado = await _FrequenciaService.NovoRegistroAsync(registro);
            var resultado = new FrequenciaEntity();
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Alterar Registro
        /// </summary>
        /// <param name="registro">Informe o registro</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] FrequenciaInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var reg = await _FrequenciaRepository.GetByCodigoAsync(registro.Codigo);

            if (reg == null)
            {
                NotificarErro("Registro não existe!");
                return CustomResponse();
            }

            //var resultado = await _FrequenciaService.AtualizarRegistroAsync(registro);
            var resultado = new FrequenciaEntity();
            return CustomResponse(resultado);
        }
    }
}

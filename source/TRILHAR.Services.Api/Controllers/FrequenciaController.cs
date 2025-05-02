using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO.Frequencia;
using TRILHAR.Business.IO.Matricula;

namespace TRILHAR.Services.Api.Controllers
{
    /// <summary>
    /// Controller.
    /// Contém todos os métodos dessa funcionalidade.
    /// </summary>
    [ApiController]
    [Route("api/frequencias")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class FrequenciaController : BaseApiController
    {
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;
        private readonly ILogger<FrequenciaEntity> _logger;
        private readonly IFrequenciaService _frequenciaService;
        private readonly IFrequenciaRepository _frequenciaRepository;
        private readonly IVFrequenciaService _vFrequenciaService;
        private readonly IVFrequenciaRepository _vFrequenciaRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notificador"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="frequenciaService"></param>
        /// <param name="frequenciaRepository"></param>
        /// <param name="vFrequenciaService"></param>
        /// <param name="vFrequenciaRepository"></param>
        public FrequenciaController(
            INotificador notificador,
            IMapper mapper,
            ILogger<FrequenciaEntity> logger,
            IFrequenciaService frequenciaService,
            IFrequenciaRepository frequenciaRepository,
            IVFrequenciaService vFrequenciaService,
            IVFrequenciaRepository vFrequenciaRepository
            ) : base(notificador)
        {
            _notificador = notificador;
            _mapper = mapper;
            _logger = logger;
            _frequenciaService = frequenciaService;
            _frequenciaRepository = frequenciaRepository;
            _vFrequenciaService = vFrequenciaService;
            _vFrequenciaRepository = vFrequenciaRepository;
        }

        /// <summary>
        /// Retorna todos os Registro
        /// </summary>
        /// <returns>Retorna todos Frequencias</returns>
        [HttpGet]//1
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FrequenciaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get()
        {
            var resultado = await _frequenciaRepository.GetAllAsync();
            if (resultado == null || !resultado.Any())
            {
                _logger.LogWarning("Registro não encontrado.");
                NotificarErro("Registro não encontrado.");
                return CustomResponse(isNotFound: true);
            }
            var frequenciaOutput = _mapper.Map<IEnumerable<FrequenciaOutput>>(resultado);
            return CustomResponse(frequenciaOutput);
        }

        /// <summary>
        /// Retorna o Registro por codigo
        /// </summary>
        /// <param name="id">Informe o id.</param>
        /// <returns>Retorna Frequencia</returns>
        [HttpGet("{id}")]//2
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FrequenciaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _frequenciaRepository.GetByCodigoAsync(id);
            if (resultado == null)
            {
                _logger.LogWarning("Frequência com ID {Id} não encontrado.", id);
                NotificarErro("Registro não encontrado.");
                return CustomResponse(isNotFound: true);
            }
            var frequenciaOutput = _mapper.Map<FrequenciaOutput>(resultado);
            return CustomResponse(frequenciaOutput);
        }

        ///// <summary>
        ///// Retorna todos por parametros e paginação
        ///// </summary>
        ///// <returns>Retorna todos Frequencias</returns>
        //[HttpGet("filtro")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<VFrequenciaOutput>))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        //public async Task<IActionResult> ListarPorFiltro([FromQuery] InputPaginado input)
        //{
        //    if (input == null || (input.IsPaginacao && (input.Page <= 0 || input.PageSize <= 0)))
        //    {
        //        NotificarErro("Filtro inválido: verifique paginação e parâmetros.");
        //        return CustomResponse();
        //    }

        //    var resultado = await _vFrequenciaRepository.GetByPaginacaoAsync(input);
        //    var frequenciaOutput = _mapper.Map<PagedResult<VFrequenciaOutput>>(resultado);
        //    return CustomResponse(frequenciaOutput);
        //}

        /// <summary>
        /// Retorna todas as frequências [Presentes + Ausentes] para o dia informado
        /// </summary>
        /// <param name="data">Informe a Data da Frequência</param>
        /// <returns>Frequencias Presentes + Frequencias Ausentes</returns>
        [HttpGet("data/{data}")]//3
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VFrequenciaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetByDateAsync([FromRoute] DateTime data)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _frequenciaService.GetByDateAsync(data);//SPFrequenciasPorData @DataFrequencia
            if (resultado == null || !resultado.Any())
            {
                NotificarErro("Registro não encontrado!");
                return CustomResponse(isNotFound: true);
            }
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna Agrupamento de Turmas e suas quantidades para o dia informado
        /// </summary>
        /// <param name="data">Informe a Data da Frequência</param>
        /// <returns></returns>
        [HttpGet("turmas/agrupadas/data/{data}")]//4
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FrequenciasTurmasAgrupadasOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetTurmasAgrupadasByDateAsync([FromRoute] DateTime data)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _frequenciaService.GetTurmasAgrupadasByDateAsync(data);//SPFrequenciasTodasTurmasAgrupadasDia @DataFrequencia
            if (resultado == null || !resultado.Any())
            {
                NotificarErro("Registro não encontrado!");
                return CustomResponse(isNotFound: true);
            }
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna todas as frequências [Presentes + Ausentes] das turma para o dia informado
        /// </summary>
        /// <param name="codigoTurma"></param>
        /// <param name="data"></param>
        /// <returns>Frequencias Presentes + Frequencias Ausentes</returns>
        [HttpGet("turmas/{codigoTurma}/data/{data}")]//5
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VFrequenciaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetByTurmasAndDateAsync([FromRoute] int codigoTurma, [FromRoute] DateTime data)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _frequenciaService.GetByTurmasAndDateAsync(codigoTurma, data);
            if (resultado == null || !resultado.Any())
            {
                NotificarErro("Registro não encontrado!");
                return CustomResponse(isNotFound: true);
            }
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna todas as frequências do aluno.
        /// </summary>
        /// <param name="codigoAluno"></param>
        /// <returns></returns>
        [HttpGet("aluno/{codigoAluno}")]//6
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VFrequenciaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetByAlunoAsync([FromRoute] int codigoAluno)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _frequenciaService.GetByAlunoAsync(codigoAluno);
            if (resultado == null || !resultado.Any())
            {
                NotificarErro("Registro não encontrado!");
                return CustomResponse(isNotFound: true);
            }
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna todas as frequências da turma.
        /// </summary>
        /// <param name="codigoTurma"></param>
        /// <returns></returns>
        [HttpGet("turmas/{codigoTurma}")]//7
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VFrequenciaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetByTurmaAsync([FromRoute] int codigoTurma)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _frequenciaService.GetByTurmaAsync(codigoTurma);
            if (resultado == null || !resultado.Any())
            {
                NotificarErro("Registro não encontrado!");
                return CustomResponse(isNotFound: true);
            }
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna todas as frequências do aluno e da turma.
        /// </summary>
        /// <param name="codigoAluno"></param>
        /// <param name="codigoTurma"></param>
        /// <returns></returns>
        [HttpGet("alunos/{codigoAluno}/turmas/{codigoTurma}")]//8
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VFrequenciaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetByAlunoAndTurmaAsync([FromRoute] int codigoAluno, [FromRoute] int codigoTurma)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _frequenciaService.GetByAlunoAndTurmaAsync(codigoAluno, codigoTurma);
            if (resultado == null || !resultado.Any())
            {
                NotificarErro("Registro não encontrado!");
                return CustomResponse(isNotFound: true);
            }
            return CustomResponse(resultado);
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
        public async Task<IActionResult> CreateFrequenciaAsync([FromBody] FrequenciaInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            _logger.LogInformation("Criando nova frequência: {@Frequencia}", registro);
            var resultado = await _frequenciaService.AddAsync(registro);
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
        public async Task<IActionResult> UpdateFrequenciaAsync(int id, [FromBody] FrequenciaInput input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var reg = await _frequenciaRepository.GetByCodigoAsync(id);
            if (reg == null)
            {
                _logger.LogWarning("Tentativa de atualização para frequência ID {Id}, mas não encontrado.", id);
                NotificarErro("Registro não existe!");
                return CustomResponse();
            }

            input.DataCadastro = reg.DataCadastro;

            _logger.LogInformation("Atualizando frequência ID {Id}: {@Input}", id, input);
            var resultado = await _frequenciaService.UpdateAsync(input);
            return CustomResponse(resultado);
        }
    }
}

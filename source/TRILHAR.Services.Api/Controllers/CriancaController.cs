using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO.Crianca;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Services.Api.Controllers
{
    /// <summary>
    /// Controller.
    /// Contém todos os métodos dessa funcionalidade.
    /// </summary>
    [ApiController]
    [Route("api/criancas")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class CriancaController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CriancaEntity> _logger;
        private readonly ICriancaService _alunoService;
        private readonly ICriancaRepository _alunoRepository;
        private readonly IMatriculaService _matriculaService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notificador"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="alunoService"></param>
        /// <param name="alunoRepository"></param>
        /// <param name="matriculaService"></param>
        /// 
        public CriancaController(
            INotificador notificador, 
            IMapper mapper, 
            ILogger<CriancaEntity> logger, 
            ICriancaService alunoService, 
            ICriancaRepository alunoRepository, 
            IMatriculaService matriculaService) : base(notificador)
        {
            _mapper = mapper;
            _logger = logger;
            _alunoService = alunoService;
            _alunoRepository = alunoRepository;
            _matriculaService = matriculaService;
        }

        /// <summary>
        /// Retorna todos os Registro
        /// </summary>
        /// <returns>Retorna todos alunos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CriancaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get()
        {
            var resultado = await _alunoService.GetAllAsync();
            if (resultado == null || !resultado.Any())
            {
                _logger.LogWarning("Registro não encontrado.");
                NotificarErro("Registro não encontrado.");
                return CustomResponse(isNotFound: true);
            }
            var alunoOutput = _mapper.Map<IEnumerable<CriancaOutput>>(resultado);
            return CustomResponse(alunoOutput);
        }

        /// <summary>
        /// Retorna o Registro por codigo
        /// </summary>
        /// <param name="id">Informe o id.</param>
        /// <returns>Retorna aluno</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CriancaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = await _alunoService.GetByCodigoAsync(id);
            if (resultado == null)
            {
                _logger.LogWarning("Aluno com ID {Id} não encontrado.", id);
                NotificarErro("Registro não encontrado.");
                return CustomResponse(isNotFound: true);
            }

            var listaMatriculas = await _matriculaService.ListarPorCodigoAluno(resultado.Codigo);
            var alunoOutput = _mapper.Map<CriancaOutput>(resultado);
            alunoOutput.Matricula = listaMatriculas?.FirstOrDefault(x => x.Ativo);
            return CustomResponse(alunoOutput);
        }

        /// <summary>
        /// Retorna todos por parametros e paginação
        /// </summary>
        /// <returns>Retorna todos alunos</returns>
        [HttpGet("filtro")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<CriancaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> ListarPorFiltro([FromQuery] CriancaPorFiltroInput input)
        {
            var alunoInput = _mapper.Map<CriancaInput>(input);
            var resultado = await _alunoService.GetByListarPorFiltroPaginacaoAsync(alunoInput);

            if (resultado == null || !resultado.Dados.Any())
            {
                _logger.LogInformation("Filtro aplicado não retornou resultados.");
                NotificarErro("Nenhum resultado encontrado.");
                return CustomResponse(isNotFound: true);
            }

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna Registro por Codigo Cadastro
        /// </summary>
        /// <param name="codigo">Informe o código cadastro.</param>
        /// <returns></returns>
        [HttpGet("codigo-cadastro/{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CriancaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetCodigoCadastro(string codigo)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _alunoService.GetByCodigoCadastroAsync(codigo);
            if (resultado == null)
            {
                _logger.LogWarning("Aluno com código de cadastro {Id} não encontrado.", codigo);
                NotificarErro("Registro não encontrado.");
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
        public async Task<IActionResult> CreateAlunoAsync([FromBody] CriancaInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            _logger.LogInformation("Criando novo aluno: {@Aluno}", registro);
            var resultado = await _alunoService.InsertAsync(registro);

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
        public async Task<IActionResult> UpdateAlunoAsync(int id, [FromBody] CriancaInput input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var reg = await _alunoRepository.GetByCodigoAsync(id);
            if (reg == null)
            {
                _logger.LogWarning("Tentativa de atualização para aluno ID {Id}, mas não encontrado.", id);
                NotificarErro("Registro não existe!");
                return CustomResponse(isNotFound: true);
            }

            input.DataCadastro = reg.DataCadastro;

            _logger.LogInformation("Atualizando aluno ID {Id}: {@Input}", id, input);
            var resultado = await _alunoService.UpdateAsync(input);
            return CustomResponse(resultado);
        }
    }
}

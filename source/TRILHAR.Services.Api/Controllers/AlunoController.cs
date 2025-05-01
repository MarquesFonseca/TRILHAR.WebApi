using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.IO.Matricula;
using TRILHAR.Business.IO.Aluno;
using TRILHAR.Business.IO.Frequencia;
using TRILHAR.Business.IO.Paginacao;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Services.Api.Controllers
{
    /// <summary>
    /// Controller.
    /// Contém todos os métodos dessa funcionalidade.
    /// </summary>
    [ApiController]
    [Route("api/alunos")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class AlunoController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AlunoEntity> _logger;
        private readonly IAlunoService _alunoService;
        private readonly IAlunoRepository _alunoRepository;
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
        public AlunoController(
            INotificador notificador, 
            IMapper mapper, 
            ILogger<AlunoEntity> logger, 
            IAlunoService alunoService, 
            IAlunoRepository alunoRepository, 
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AlunoOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get()
        {
            var resultado = await _alunoService.GetAllAsync();
            if (resultado == null || !resultado.Any())
            {
                NotificarErro("Registro não encontrado!");
                return CustomResponse(isNotFound: true);
            }
            var alunoOutput = _mapper.Map<IEnumerable<AlunoOutput>>(resultado);
            return CustomResponse(alunoOutput);
        }

        /// <summary>
        /// Retorna o Registro por codigo
        /// </summary>
        /// <param name="id">Informe o id.</param>
        /// <returns>Retorna aluno</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlunoOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = await _alunoService.GetByCodigoAsync(id);
            if (resultado == null)
            {
                NotificarErro("Registro não encontrado.");
                return CustomResponse(isNotFound: true);
            }
            var listaMatriculas = await _matriculaService.ListarPorCodigoAluno(resultado.Codigo);            
            var alunoOutput = _mapper.Map<AlunoOutput>(resultado);
            alunoOutput.Matricula = listaMatriculas?.FirstOrDefault(x => x.Ativo);
            return CustomResponse(alunoOutput);
        }

        /// <summary>
        /// Retorna todos por parametros e paginação
        /// </summary>
        /// <returns>Retorna todos alunos</returns>
        [HttpGet("filtro")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<AlunoOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> ListarPorFiltro([FromQuery] AlunoPorFiltroInput input)
        {
            var alunoInput = _mapper.Map<AlunoInput>(input);
            var resultado = await _alunoService.GetByListarPorFiltroPaginacaoAsync(alunoInput);
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna Registro por Codigo Cadastro
        /// </summary>
        /// <param name="id">Informe o código cadastro.</param>
        /// <returns></returns>
        [HttpGet("CodigoCadastro/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlunoOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetCodigoCadastro(string id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _alunoService.GetByCodigoCadastroAsync(id);

            if (resultado == null)
            {
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AlunoInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

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
        public async Task<IActionResult> Put(int id, [FromBody] AlunoInput input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var reg = await _alunoRepository.GetByCodigoAsync(id);
            if (reg == null)
            {
                NotificarErro("Registro não existe!");
                return CustomResponse();
            }

            input.DataCadastro = reg.DataCadastro;
            var resultado = await _alunoService.UpdateAsync(input);
            return CustomResponse(resultado);
        }
    }
}

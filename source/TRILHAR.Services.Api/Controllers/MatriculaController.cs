using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO.Matricula;

namespace TRILHAR.Services.Api.Controllers
{
    /// <summary>
    /// Controller.
    /// Contém todos os métodos dessa funcionalidade.
    /// </summary>
    [ApiController]
    [Route("api/matriculas")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class MatriculaController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MatriculaEntity> _logger;
        private readonly IMatriculaService _matriculaService;
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IVMatriculaService _vMatriculaService;
        private readonly IVMatriculaRepository _vMatriculaRepository;
        private readonly IFrequenciaService _frequenciaService;
        private readonly IFrequenciaRepository _frequenciaRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notificador"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="matriculaService"></param>
        /// <param name="matriculaRepository"></param>
        /// <param name="vMatriculaService"></param>
        /// <param name="vMatriculaRepository"></param>
        /// <param name="frequenciaService"></param>
        /// <param name="frequenciaRepository"></param>
        public MatriculaController(
            INotificador notificador,
            IMapper mapper,
            ILogger<MatriculaEntity> logger,
            IMatriculaService matriculaService,
            IMatriculaRepository matriculaRepository,
            IVMatriculaService vMatriculaService,
            IVMatriculaRepository vMatriculaRepository,
            IFrequenciaService frequenciaService,
            IFrequenciaRepository frequenciaRepository) : base(notificador)
        {
            _mapper = mapper;
            _logger = logger;
            _matriculaService = matriculaService;
            _matriculaRepository = matriculaRepository;
            _vMatriculaService = vMatriculaService;
            _vMatriculaRepository = vMatriculaRepository;
            _frequenciaService = frequenciaService;
            _frequenciaRepository = frequenciaRepository;
        }

        /// <summary>
        /// Retorna todos os Registro
        /// </summary>
        /// <returns>Retorna todos Matriculas</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VMatriculaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get()
        {
            var resultado = await _vMatriculaRepository.GetAllAsync();
            if (resultado == null || !resultado.Any())
            {
                _logger.LogWarning("Registro não encontrado.");
                NotificarErro("Registro não encontrado.");
                return CustomResponse(isNotFound: true);
            }
            var matriculaOutput = _mapper.Map<IEnumerable<VMatriculaOutput>>(resultado);
            return CustomResponse(matriculaOutput);
        }

        ///// <summary>
        ///// Retorna todos por parametros e paginação
        ///// </summary>
        ///// <returns>Retorna todos Matriculas</returns>
        //[HttpPost("filtro")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MatriculaEntity))]
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

        //    if (input.IsPaginacao && input.Page == 0)
        //    {
        //        return BadRequest("O filtro 'Page 'não pode ser 0.");
        //    }

        //    if (input.IsPaginacao && input.PageSize == 0)
        //    {
        //        return BadRequest("O filtro 'PageSize 'não pode ser 0.");
        //    }


        //    var resultado = await _vMatriculaRepository.GetByPaginacaoAsync(input);
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
        /// <returns>Retorna Matricula</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VMatriculaOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _vMatriculaRepository.GetByCodigoAsync(id);
            if (resultado == null)
            {
                _logger.LogWarning("Matricula com ID {Id} não encontrado.", id);
                NotificarErro("Registro não encontrado.");
                return CustomResponse(isNotFound: true);
            }
            var matriculaOutput = _mapper.Map<VMatriculaOutput>(resultado);
            return CustomResponse(matriculaOutput);
        }

        /// <summary>
        /// Retorna o Registro por CodigoAluno
        /// </summary>
        /// <param name="codigoAluno"></param>
        /// <returns>Retorna Matricula</returns>
        [HttpGet("aluno/{codigoAluno}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VMatriculaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> ListarPorCodigoAluno(int codigoAluno)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _vMatriculaService.ListarPorCodigoAluno(codigoAluno);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna o Registro por CodigoTurma
        /// </summary>
        /// <param name="codigoTurma"></param>
        /// <returns>Retorna Matricula</returns>
        [HttpGet("turma/{codigoTurma}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VMatriculaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> ListarPorCodigoTurma(int codigoTurma)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _vMatriculaService.ListarPorCodigoTurma(codigoTurma);
            if (resultado == null || !resultado.Any())
            {
                NotificarErro("Registro não encontrado.");
                return CustomResponse(isNotFound: true);
            }

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna o Registro por CodigoAluno e CodigoTurma
        /// </summary>
        /// <param name="codigoAluno"></param>
        /// <param name="codigoTurma"></param>
        /// <returns>Retorna Matricula</returns>
        [HttpGet("aluno/{codigoAluno}/turma/{codigoTurma}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VMatriculaOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> ListarPorCodigoAlunoCodigoTurma([FromRoute] int codigoAluno, [FromRoute] int codigoTurma)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _vMatriculaService.ListarPorCodigoAlunoCodigoTurma(codigoAluno, codigoTurma);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> CreateMatriculaAsync([FromBody] MatriculaInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            _logger.LogInformation("Criando nova matrícula: {@Matricula}", registro);
            var resultado = await _matriculaService.InsertAsync(registro);
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
        public async Task<IActionResult> UpdateMatriculaAsync(int id, [FromBody] MatriculaInput input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);            

            var listaMatriculasAluno = await _matriculaService.ListarPorCodigoAluno(input.CodigoAluno);
            if (listaMatriculasAluno != null)
            {
                //desativa todas as matriculas do aluno que estão ativas
                foreach (var item in listaMatriculasAluno)
                {
                    if (item.Ativo == true)
                    {
                        var itemUpdate = _mapper.Map<MatriculaInput>(item);
                        itemUpdate.Ativo = false;
                        itemUpdate.DataAtualizacao = DateTime.Now;
                        await _matriculaService.UpdateAsync(itemUpdate);
                    }

                    //antes de desativar, ver se possue frequencia... se não houver nenhuma apagar a matricula ao inves de alterar para inativo
                    var freqAlunoTurma = await _frequenciaService.GetByAlunoAndTurmaAsync(input.CodigoAluno, item.CodigoTurma);
                    if (freqAlunoTurma == null || !freqAlunoTurma.Any(x => x.Presenca == true))
                    {
                        //se não existir nenhuma frequencia remove a matricula
                        var itemDelete = _mapper.Map<MatriculaEntity>(item);
                        await _matriculaRepository.DeleteAsync(itemDelete);
                    }
                }
            }

            if (input.CodigoTurma == 0)
            {
                //retornar pois não foi selecionado a turma portanto o usuario que deixar sem matricula. 
                //como já desativou todas acima não existe matricula ativa no momento. 
                //retornar...
                return CustomResponse(true);
            }

            var listaMatriculasAlunoTurma = await _matriculaService.ListarPorCodigoAlunoCodigoTurma(input.CodigoAluno, input.CodigoTurma);
            if (listaMatriculasAlunoTurma != null && listaMatriculasAlunoTurma.Any())
            {
                foreach (var item in listaMatriculasAluno)
                {
                    var itemUpdate = _mapper.Map<MatriculaInput>(item);
                    itemUpdate.Ativo = true;
                    itemUpdate.DataAtualizacao = DateTime.Now;
                    var retorno = await _matriculaService.UpdateAsync(itemUpdate);
                    return CustomResponse(retorno);
                }
            }
            else
            {
                //não existe, cria
                input.Codigo = 0;
                //input.CodigoAluno = 0;
                //input.CodigoTurma = 0;
                input.Ativo = true;
                input.CodigoUsuarioLogado = null;
                input.DataAtualizacao = input.DataCadastro = DateTime.Now;
                var retorno = await _matriculaService.InsertAsync(input);
                return CustomResponse(retorno);
            }

            return CustomResponse(false);
        }
    }
}

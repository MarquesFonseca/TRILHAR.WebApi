using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.IO.Matricula;
using TRILHAR.Business.IO.Turma;
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
        private readonly IMapper _mapper;
        private readonly ILogger<MatriculaEntity> _logger;
        private readonly IMatriculaService _matriculaService;
        private readonly IMatriculaRepository _matriculaRepository;
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
        /// <param name="vMatriculaRepository"></param>
        /// <param name="frequenciaService"></param>
        /// <param name="frequenciaRepository"></param>
        public MatriculaController(
            INotificador notificador,
            IMapper mapper,
            ILogger<MatriculaEntity> logger,
            IMatriculaService matriculaService,
            IMatriculaRepository matriculaRepository,
            IVMatriculaRepository vMatriculaRepository,
            IFrequenciaService frequenciaService,
            IFrequenciaRepository frequenciaRepository) : base(notificador)
        {
            _mapper = mapper;
            _logger = logger;
            _matriculaService = matriculaService;
            _matriculaRepository = matriculaRepository;
            _vMatriculaRepository = vMatriculaRepository;
            _frequenciaService = frequenciaService;
            _frequenciaRepository = frequenciaRepository;
        }

        /// <summary>
        /// Retorna todos os Registro
        /// </summary>
        /// <returns>Retorna todos Matriculas</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultado = await _matriculaRepository.GetAllAsync();
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna todos por parametros e paginação
        /// </summary>
        /// <returns>Retorna todos Matriculas</returns>
        [HttpPost("ListarPorFiltro")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<MatriculaEntity>))]
        public async Task<IActionResult> ListarPorFiltro(
            [FromBody] InputPaginado input)
        {
            if (input == null)
            {
                return BadRequest("O filtro não pode ser nulo.");
            }

            if (input.IsPaginacao && input.Page == 0)
            {
                return BadRequest("O filtro 'Page 'não pode ser 0.");
            }

            if (input.IsPaginacao && input.PageSize == 0)
            {
                return BadRequest("O filtro 'PageSize 'não pode ser 0.");
            }

            //var resultado = await _MatriculaRepository.GetByPaginacaoAsync(input);
            var resultado = await _vMatriculaRepository.GetByPaginacaoAsync(input);
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
            var resultado = await _matriculaRepository.GetByCodigoAsync(id);
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna o Registro por CodigoAluno e CodigoTurma
        /// </summary>
        /// <param name="CodigoAluno"></param>
        /// <param name="CodigoTurma"></param>
        /// <returns>Retorna Matricula</returns>
        [HttpGet("ListarPorCodigoAlunoCodigoTurma/{CodigoAluno}/{CodigoTurma}")]
        public async Task<IActionResult> ListarPorCodigoAlunoCodigoTurma(int CodigoAluno, int CodigoTurma)
        {
            if (CodigoAluno == 0 && CodigoTurma == 0)
            {
                return CustomResponse(ModelState);
            }

            var resultado = await _matriculaService.ListarPorCodigoAlunoCodigoTurma(CodigoAluno, CodigoTurma);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna o Registro por CodigoAluno
        /// </summary>
        /// <param name="CodigoAluno"></param>
        /// <returns>Retorna Matricula</returns>
        [HttpGet("ListarPorCodigoAluno/{CodigoAluno}")]
        public async Task<IActionResult> ListarPorCodigoAluno(int CodigoAluno)
        {
            if (CodigoAluno == 0)
            {
                return CustomResponse(ModelState);
            }

            var resultado = await _matriculaService.ListarPorCodigoAluno(CodigoAluno);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna o Registro por CodigoTurma
        /// </summary>
        /// <param name="CodigoTurma"></param>
        /// <returns>Retorna Matricula</returns>
        [HttpGet("ListarPorCodigoTurma/{CodigoTurma}")]
        public async Task<IActionResult> ListarPorCodigoTurma(int CodigoTurma)
        {
            if (CodigoTurma == 0)
            {
                return CustomResponse(ModelState);
            }

            var resultado = await _matriculaService.ListarPorCodigoTurma(CodigoTurma);

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

            var resultado = await _matriculaService.InsertAsync(registro);
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Alterar Registro
        /// </summary>
        /// <param name="input">Informe o registro</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MatriculaInput input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var listaMatriculasAluno = await _matriculaService.ListarPorCodigoAluno(input.CodigoAluno);

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
                if (!freqAlunoTurma.Any())
                {
                    //se não existir nenhuma frequencia remove a matricula
                    var itemDelete = _mapper.Map<MatriculaEntity>(item);
                    await _matriculaRepository.DeleteAsync(itemDelete);
                }
            }

            var listaMatriculasAlunoTurma = await _matriculaService.ListarPorCodigoAlunoCodigoTurma(input.CodigoAluno, input.CodigoTurma);
            if (listaMatriculasAlunoTurma.Any())
            {
                foreach (var item in listaMatriculasAluno)
                {
                    var itemUpdate = _mapper.Map<MatriculaInput>(item);
                    itemUpdate.Ativo = true;
                    itemUpdate.DataAtualizacao = DateTime.Now;
                    await _matriculaService.UpdateAsync(itemUpdate);
                    return CustomResponse(true);
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
                await _matriculaService.InsertAsync(input);
                return CustomResponse(true);
            }

            return CustomResponse(false);
        }
    }
}

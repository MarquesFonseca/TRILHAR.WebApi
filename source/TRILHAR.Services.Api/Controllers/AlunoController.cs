using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO.Aluno;
using TRILHAR.Business.IO.Paginacao;
using TRILHAR.Business.Pagination;
using TRILHAR.Infra.Data.Repositories;

namespace TRILHAR.Services.Api.Controllers
{
    /// <summary>
    /// Categoria.
    /// Contém todos os métodos dessa funcionalidade.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AlunoController : BaseApiController
    {
        private readonly ILogger<AlunoEntity> _logger;
        private readonly IAlunoService _alunoService;
        private readonly IAlunoRepository _alunoRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notificador"></param>
        /// <param name="logger"></param>
        /// <param name="alunoService"></param>
        /// <param name="alunoRepository"></param>
        /// 
        public AlunoController(
            INotificador notificador,
            ILogger<AlunoEntity> logger,
            IAlunoService alunoService,
            IAlunoRepository alunoRepository
            ) : base(notificador)
        {
            _logger = logger;
            _alunoService = alunoService;
            _alunoRepository = alunoRepository;
        }

        /// <summary>
        /// Retorna todos os Aluno
        /// </summary>
        /// <returns>Retorna todos alunos</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultado = await _alunoRepository.GetAllAsync();
            return CustomResponse(resultado);
        }

        ///// <summary>
        ///// Retorna todos com paginação
        ///// </summary>
        ///// <returns>Retorna todos alunos</returns>
        //[HttpGet("pagina/{page}/{pageSize}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<AlunoEntity>))]        
        //public async Task<IActionResult> Get(
        //    [FromRoute] [Required] int page, 
        //    [FromRoute][Required] int pageSize)
        //{
        //    var resultado = await _alunoRepository.GetAllAsync();
        //    if (OperacaoValida())
        //    {
        //        return Ok(resultado);
        //    }
        //    return CustomResponse(resultado);
        //}

        /// <summary>
        /// Retorna todos por parametros e paginação
        /// </summary>
        /// <returns>Retorna todos alunos</returns>
        [HttpPost("ListarPorFiltro")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<AlunoEntity>))]
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

            var resultado = await _alunoRepository.GetByPaginacaoAsync(input);
            if (OperacaoValida())
            {
                return Ok(resultado);
            }
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna Aluno por codigo
        /// </summary>
        /// <param name="id">Informe o id.</param>
        /// <returns>Retorna aluno</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = await _alunoRepository.GetByCodigoAsync(id);
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna Aluno por Codigo Cadastro
        /// </summary>
        /// <param name="CodigoCadastro">Informe o código cadastro.</param>
        /// <returns></returns>
        [HttpGet("CodigoCadastro/{CodigoCadastro}")]
        public async Task<IActionResult> GetCodigoCadastro(string CodigoCadastro)
        {
            var resultado = await _alunoService.RetornaByCodigoCadastroAsync(CodigoCadastro);
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Incluir
        /// </summary>
        /// <param name="registro">Informe o registro</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AlunoInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var resultado = await _alunoService.NovoRegistroAsync(registro);
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Alteração
        /// </summary>
        /// <param name="registro">Informe o registro</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AlunoInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var reg = await _alunoRepository.GetByCodigoAsync(registro.Codigo);

            if (reg == null)
            {
                NotificarErro("Registro não existe!");
                return CustomResponse();
            }

            var resultado = await _alunoService.AtualizarRegistroAsync(registro);
            return CustomResponse(resultado);
        }
    }
}

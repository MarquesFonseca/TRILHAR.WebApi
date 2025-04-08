using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.IO.Aluno;
using TRILHAR.Business.IO.Paginacao;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Services.Api.Controllers
{
    /// <summary>
    /// Controller.
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
        public AlunoController(INotificador notificador,ILogger<AlunoEntity> logger, IAlunoService alunoService, IAlunoRepository alunoRepository
            ) : base(notificador)
        {
            _logger = logger;
            _alunoService = alunoService;
            _alunoRepository = alunoRepository;
        }

        /// <summary>
        /// Retorna todos os Registro
        /// </summary>
        /// <returns>Retorna todos alunos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AlunoOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var resultado = await _alunoService.GetAllAsync();
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna o Registro por codigo
        /// </summary>
        /// <param name="id">Informe o id.</param>
        /// <returns>Retorna aluno</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlunoOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = await _alunoService.GetByCodigoAsync(id);
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna todos por parametros e paginação
        /// </summary>
        /// <returns>Retorna todos alunos</returns>
        [HttpGet("ListarPorFiltro")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<AlunoOutput>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListarPorFiltro(
            [FromQuery] int? Codigo,
            //[FromQuery] string? CodigoCadastro,
            [FromQuery] string? NomeCrianca,
            [FromQuery] DateTime? DataNascimento,
            [FromQuery] DateTime? DataNascimentoInicial,
            [FromQuery] DateTime? DataNascimentoFinal,
            [FromQuery] string? NomeMae,
            [FromQuery] string? NomePai,
            [FromQuery] string? OutroResponsavel,
            [FromQuery] bool? Alergia,
            [FromQuery] bool? RestricaoAlimentar,
            [FromQuery] bool? DeficienciaOuSituacaoAtipica,
            [FromQuery] bool? Batizado,
            [FromQuery] DateTime? DataBatizadoInicial,
            [FromQuery] DateTime? DataBatizadoFinal,
            [FromQuery] DateTime? DataAtualizacaoInicial,
            [FromQuery] DateTime? DataAtualizacaoFinal,
            [FromQuery] DateTime? DataCadastroInicial,
            [FromQuery] DateTime? DataCadastroFinal,
            [FromQuery] bool? Ativo,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] bool isPaginacao = true)
        {

            AlunoInput input = new AlunoInput() 
            {
                Codigo = Codigo,
                NomeCrianca = NomeCrianca,
                DataNascimento = DataNascimento,
                DataNascimentoInicial = DataNascimentoInicial,
                DataNascimentoFinal = DataNascimentoFinal,
                NomeMae = NomeMae,
                NomePai = NomePai,
                OutroResponsavel = OutroResponsavel,
                Alergia = Alergia,
                RestricaoAlimentar = RestricaoAlimentar,
                DeficienciaOuSituacaoAtipica = DeficienciaOuSituacaoAtipica,
                Batizado = Batizado,
                DataBatizadoInicial = DataBatizadoInicial,
                DataBatizadoFinal = DataBatizadoFinal,
                DataAtualizacaoInicial = DataAtualizacaoInicial,
                DataAtualizacaoFinal = DataAtualizacaoFinal,
                DataCadastroInicial = DataCadastroInicial,
                DataCadastroFinal = DataCadastroFinal,
                Ativo = Ativo
            };
            var resultado = await _alunoService.GetByListarPorFiltroPaginacaoAsync(input, page, pageSize, isPaginacao);

            if (OperacaoValida())
            {
                return Ok(resultado);
            }
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna Registro por Codigo Cadastro
        /// </summary>
        /// <param name="id">Informe o código cadastro.</param>
        /// <returns></returns>
        [HttpGet("CodigoCadastro/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlunoOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCodigoCadastro(string id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            
            var resultado = await _alunoService.GetByCodigoCadastroAsync(id);
            
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
        /// <param name="registro">Informe o registro</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] AlunoInput registro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var reg = await _alunoService.GetByCodigoAsync(registro.Codigo);

            if (reg == null)
            {
                NotificarErro("Registro não existe!");
                return CustomResponse();
            }

            var resultado = await _alunoService.UpdateAsync(registro);
            return CustomResponse(resultado);
        }
    }
}

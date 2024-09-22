using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.Interfaces.Usuario;
using System.Threading.Tasks;
using TRILHAR.Services.Api.Controllers;
using System.Collections.Generic;
using TRILHAR.Business.Entities;
using Microsoft.Extensions.Logging;
using TRILHAR.Business.IO.Aluno;

namespace TRILHAR.Services.Api.Controllers
{
    /// <summary>
    /// Categoria.
    /// Contém todos os métodos dessa funcionalidade.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : BaseApiController
    {
        private readonly ILogger<AlunoEntity> _logger;
        private readonly IAlunoService _alunoService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notificador"></param>
        /// <param name="logger"></param>
        /// <param name="alunoService"></param>
        /// 
        public AlunoController(
            INotificador notificador,
            ILogger<AlunoEntity> logger,
            IAlunoService alunoService
            ) : base(notificador)
        {
            _logger = logger;
            _alunoService = alunoService;
        }

        /// <summary>
        /// Retorna todos os Aluno
        /// </summary>
        /// <returns>Retorna todos alunos</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var resultado = await _alunoService.RetornaAll();
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna Aluno por codigo
        /// </summary>
        /// <param name="id">Informe o id.</param>
        /// <returns>Retorna aluno</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var resultado = await _alunoService.RetornaByCodigo(id);
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Retorna Aluno por Codigo Cadastro
        /// </summary>
        /// <param name="CodigoCadastro">Informe o código cadastro.</param>
        /// <returns></returns>
        [HttpGet("CodigoCadastro/{CodigoCadastro}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCodigoCadastro(string CodigoCadastro)
        {
            var resultado = await _alunoService.RetornaByCodigoCadastro(CodigoCadastro);
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

            var resultado = await _alunoService.NovoRegistro(registro);
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

            var reg = await _alunoService.RetornaByCodigo(registro.Codigo);

            if (reg == null)
            {
                NotificarErro("Registro não existe!");
                return CustomResponse();
            }

            var resultado = await _alunoService.AtualizarRegistro(registro);
            return CustomResponse(resultado);
        }
    }
}

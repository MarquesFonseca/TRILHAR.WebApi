using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Extensions;
using TRILHAR.Business.Interfaces;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.IO.Matricula;
using TRILHAR.Business.IO.VMatricula;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.Services
{
    public class VMatriculaService : ServiceGenericsBase<VMatriculaEntity>, IVMatriculaService
    {
        private readonly IObjectExtensionGenerics<VMatriculaEntity> _objectExtensionGenerics;
        private readonly ICriancaRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IVMatriculaRepository _vMatriculaRepository;
        private readonly IFrequenciaRepository _frequenciaRepository;
        private readonly IVFrequenciaRepository _vFrequenciaRepository;

        public VMatriculaService(
            INotificador notificador,
            IObjectExtensionGenerics<VMatriculaEntity> objectExtensionGenerics,
            ICriancaRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMatriculaRepository matriculaRepository,
            IVMatriculaRepository vMatriculaRepository,
            IFrequenciaRepository frequenciaRepository,
            IVFrequenciaRepository vFrequenciaRepository,
            IMapper mapper) : base(notificador, mapper, vMatriculaRepository)
        {
            _objectExtensionGenerics = objectExtensionGenerics;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _matriculaRepository = matriculaRepository;
            _vMatriculaRepository = vMatriculaRepository;
            _frequenciaRepository = frequenciaRepository;
            _vFrequenciaRepository = vFrequenciaRepository;
        }

        public async Task<IEnumerable<VMatriculaOutput>?> ListarPorCodigoAlunoCodigoTurma(int codigoAluno, int codigoTurma)
        {
            var inputCondicaoParametros = new InputCondicaoParametros
            {
                Condicao = "CodigoAluno = @CodigoAluno AND CodigoTurma = @CodigoTurma",
                Parametros = new Dictionary<string, object?>
                {
                    { "@CodigoAluno", codigoAluno },
                    { "@CodigoTurma", codigoTurma }
                }
            };

            var listaMatriculas = await _vMatriculaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);

            var resultado = _mapper.Map<IEnumerable<VMatriculaOutput>>(listaMatriculas);

            return resultado.Any() ? resultado : null;
        }

        public async Task<IEnumerable<VMatriculaOutput>?> ListarPorCodigoAluno(int codigoAluno)
        {
            var inputCondicaoParametros = new InputCondicaoParametros
            {
                Condicao = "CodigoAluno = @CodigoAluno",
                Parametros = new Dictionary<string, object?>
                {
                    { "@CodigoAluno", codigoAluno }
                }
            };

            var listaMatriculas = await _vMatriculaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);

            var resultado = _mapper.Map<IEnumerable<VMatriculaOutput>>(listaMatriculas);

            return resultado.Any() ? resultado : null;
        }

        public async Task<IEnumerable<VMatriculaOutput>?> ListarPorCodigoTurma(int codigoTurma)
        {
            var inputCondicaoParametros = new InputCondicaoParametros
            {
                Condicao = "CodigoTurma = @CodigoTurma",
                Parametros = new Dictionary<string, object?>
                {
                    { "@CodigoTurma", codigoTurma }
                }
            };

            var listaMatriculas = await _vMatriculaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);

            var resultado = _mapper.Map<IEnumerable<VMatriculaOutput>>(listaMatriculas);

            return resultado.Any() ? resultado : null;
        }

        public async Task<PagedResult<VMatriculaOutput>> GetByListarPorFiltroPaginacaoAsync(VMatriculaInput input)
        {
            var query = await _vMatriculaRepository.GetAllAsync();
            query = query.OrderByDescending(x => x.AlunoCodigoCadastro);

            if (input.Codigo > 0)
            {
                query = query.Where(x => x.Codigo == input.Codigo);
            }            
            if (input.CodigoAluno.HasValue)
            {
                query = query.Where(x => x.CodigoAluno == input.CodigoAluno.Value);
            }
            if (input.CodigoTurma.HasValue)
            {
                query = query.Where(x => x.CodigoTurma == input.CodigoTurma.Value);
            }
            if (input.Ativo.HasValue)
            {
                query = query.Where(x => x.Ativo == input.Ativo.Value);
            }
            if (input.CodigoUsuarioLogado.HasValue)
            {
                query = query.Where(x => x.CodigoUsuarioLogado == input.CodigoUsuarioLogado.Value);
            }
            if (input.DataCadastro.HasValue)
            {
                query = query.Where(x => x.DataCadastro != null &&
                                         x.DataCadastro.Value.Date == input.DataCadastro.Value);
            }
            if (input.DataAtualizacao.HasValue)
            {
                query = query.Where(x => x.DataAtualizacao != null &&
                                         x.DataAtualizacao.Value.Date == input.DataAtualizacao.Value);
            }
            if (!string.IsNullOrEmpty(input.AlunoCodigoCadastro))
            {
                query = query.Where(x => x.AlunoCodigoCadastro == input.AlunoCodigoCadastro);
            }
            if (!string.IsNullOrEmpty(input.AlunoNomeCrianca))
            {
                var nomeFiltro = input.AlunoNomeCrianca.RemoverAcentos().ToLower();
                query = query.Where(x => x.AlunoNomeCrianca != null &&
                                         x.AlunoNomeCrianca.RemoverAcentos().ToLower().Contains(nomeFiltro));
            }
            if (input.AlunoDataNascimento.HasValue)
            {
                query = query.Where(x => x.AlunoDataNascimento != null &&
                                         x.AlunoDataNascimento.Value.Date == input.AlunoDataNascimento.Value.Date);
            }
            if (input.DataNascimentoInicial.HasValue)
            {
                query = query.Where(x => x.AlunoDataNascimento != null &&
                                         x.AlunoDataNascimento >= input.DataNascimentoInicial.Value);
            }
            if (input.DataNascimentoFinal.HasValue)
            {
                query = query.Where(x => x.AlunoDataNascimento != null &&
                                         x.AlunoDataNascimento <= input.DataNascimentoFinal.Value);
            }
            if (!string.IsNullOrEmpty(input.AlunoNomeMae))
            {
                var nomeFiltro = input.AlunoNomeMae.RemoverAcentos().ToLower();
                query = query.Where(x => x.AlunoNomeMae != null &&
                                         x.AlunoNomeMae.RemoverAcentos().ToLower().Contains(nomeFiltro));
            }
            if (!string.IsNullOrEmpty(input.AlunoNomePai))
            {
                var nomeFiltro = input.AlunoNomePai.RemoverAcentos().ToLower();
                query = query.Where(x => x.AlunoNomePai != null &&
                                         x.AlunoNomePai.RemoverAcentos().ToLower().Contains(nomeFiltro));
            }
            if (!string.IsNullOrEmpty(input.AlunoOutroResponsavel))
            {
                var nomeFiltro = input.AlunoOutroResponsavel.RemoverAcentos().ToLower();
                query = query.Where(x => x.AlunoOutroResponsavel != null &&
                                         x.AlunoOutroResponsavel.RemoverAcentos().ToLower().Contains(nomeFiltro));
            }
            if (!string.IsNullOrEmpty(input.AlunoTelefone))
            {
                var nomeFiltro = input.AlunoTelefone.RemoverAcentos().ToLower();
                query = query.Where(x => x.AlunoTelefone != null &&
                                         x.AlunoTelefone.RemoverAcentos().ToLower().Contains(nomeFiltro));
            }
            if (!string.IsNullOrEmpty(input.AlunoEnderecoEmail))
            {
                var nomeFiltro = input.AlunoEnderecoEmail.ToLower().Trim();
                query = query.Where(x => x.AlunoEnderecoEmail != null &&
                                         x.AlunoEnderecoEmail.ToLower().Trim().Contains(nomeFiltro));
            }
            if (input.AlunoAlergia.HasValue)
            {
                query = query.Where(x => x.AlunoAlergia == input.AlunoAlergia.Value);
            }
            if (!string.IsNullOrEmpty(input.AlunoDescricaoAlergia))
            {
                var nomeFiltro = input.AlunoDescricaoAlergia.RemoverAcentos().ToLower();
                query = query.Where(x => x.AlunoDescricaoAlergia != null &&
                                         x.AlunoDescricaoAlergia.RemoverAcentos().ToLower().Contains(nomeFiltro));
            }
            if (input.AlunoRestricaoAlimentar.HasValue)
            {
                query = query.Where(x => x.AlunoRestricaoAlimentar == input.AlunoRestricaoAlimentar.Value);
            }
            if (!string.IsNullOrEmpty(input.AlunoDescricaoRestricaoAlimentar))
            {
                var nomeFiltro = input.AlunoDescricaoRestricaoAlimentar.RemoverAcentos().ToLower();
                query = query.Where(x => x.AlunoDescricaoRestricaoAlimentar != null &&
                                         x.AlunoDescricaoRestricaoAlimentar.RemoverAcentos().ToLower().Contains(nomeFiltro));
            }
            if (input.AlunoDeficienciaOuSituacaoAtipica.HasValue)
            {
                query = query.Where(x => x.AlunoDeficienciaOuSituacaoAtipica == input.AlunoDeficienciaOuSituacaoAtipica.Value);
            }
            if (!string.IsNullOrEmpty(input.AlunoDescricaoDeficiencia))
            {
                var nomeFiltro = input.AlunoDescricaoDeficiencia.RemoverAcentos().ToLower();
                query = query.Where(x => x.AlunoDescricaoDeficiencia != null &&
                                         x.AlunoDescricaoDeficiencia.RemoverAcentos().ToLower().Contains(nomeFiltro));
            }
            if (input.AlunoBatizado.HasValue)
            {
                query = query.Where(x => x.AlunoBatizado == input.AlunoBatizado.Value);
            }
            if (input.AlunoDataBatizado.HasValue)
            {
                query = query.Where(x => x.AlunoDataBatizado != null &&
                                         x.AlunoDataBatizado.Value.Date == input.AlunoDataBatizado.Value);
            }
            if (input.DataBatizadoInicial.HasValue)
            {
                query = query.Where(x => x.AlunoDataBatizado != null &&
                                         x.AlunoDataBatizado.Value.Date == input.DataBatizadoInicial.Value);
            }
            if (input.DataBatizadoFinal.HasValue)
            {
                query = query.Where(x => x.AlunoDataBatizado != null &&
                                         x.AlunoDataBatizado.Value.Date == input.DataBatizadoFinal.Value);
            }
            if (input.AlunoAtivo.HasValue)
            {
                query = query.Where(x => x.AlunoAtivo == input.AlunoAtivo.Value);
            }
            if (input.DataAtualizacaoInicial.HasValue)
            {
                query = query.Where(x => x.DataAtualizacao != null &&
                                         x.DataAtualizacao.Value.Date == input.DataAtualizacaoInicial.Value);
            }
            if (input.DataAtualizacaoFinal.HasValue)
            {
                query = query.Where(x => x.DataAtualizacao != null &&
                                         x.DataAtualizacao.Value.Date == input.DataAtualizacaoFinal.Value);
            }            
            if (input.DataCadastroInicial.HasValue)
            {
                query = query.Where(x => x.DataCadastro != null &&
                                         x.DataCadastro.Value.Date == input.DataCadastroInicial.Value);
            }
            if (input.DataCadastroFinal.HasValue)
            {
                query = query.Where(x => x.DataCadastro != null &&
                                         x.DataCadastro.Value.Date == input.DataCadastroFinal.Value);
            }

            var listaSemDuplicidade = query.Distinct(
                new GenericComparer<VMatriculaEntity>(
                    (x, y) => x.Codigo == y.Codigo && x.AlunoCodigoCadastro == y.AlunoCodigoCadastro,
                    obj => HashCode.Combine(obj.Codigo, obj.AlunoCodigoCadastro)
                )
            ).ToList();

            var retorno = _vMatriculaRepository.RetornaPagedResultAsync(listaSemDuplicidade, input.page, input.pageSize, input.isPaginacao);
            var retornoAlunoOutput = new PagedResult<VMatriculaOutput>()
            {
                Dados = _mapper.Map<List<VMatriculaOutput>>(retorno.Dados),
                PaginaAtual = retorno.PaginaAtual,
                TamanhoPagina = retorno.TamanhoPagina,
                TotalItens = retorno.TotalItens,
                TotalPaginas = retorno.TotalPaginas
            };

            return retornoAlunoOutput;
        }
    }
}

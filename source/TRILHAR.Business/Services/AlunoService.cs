using AutoMapper;
using System.Globalization;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Extensions;
using TRILHAR.Business.Interfaces;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO.Aluno;
using TRILHAR.Business.Pagination;


namespace TRILHAR.Business.Services
{
    public class AlunoService : ServiceGenericsBase<AlunoEntity>, IAlunoService
    {
        private readonly IObjectExtensionGenerics<AlunoEntity> _objectExtensionGenerics;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IMatriculaService _matriculaService;
        private readonly IVMatriculaRepository _vMatriculaRepository;
        private readonly IFrequenciaRepository _frequenciaRepository;
        private readonly IVFrequenciaRepository _vFrequenciaRepository;

        public AlunoService(
            INotificador notificador,
            IObjectExtensionGenerics<AlunoEntity> objectExtensionGenerics,
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMatriculaRepository matriculaRepository,
            IMatriculaService matriculaService,
            IVMatriculaRepository vMatriculaRepository,
            IFrequenciaRepository frequenciaRepository,
            IVFrequenciaRepository vFrequenciaRepository,
            IMapper mapper) : base(notificador, mapper, alunoRepository)
        {
            _objectExtensionGenerics = objectExtensionGenerics;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _matriculaRepository = matriculaRepository;
            _matriculaService = matriculaService;
            _vMatriculaRepository = vMatriculaRepository;
            _frequenciaRepository = frequenciaRepository;
            _vFrequenciaRepository = vFrequenciaRepository;
        }

        public async Task<AlunoOutput?> GetByCodigoCadastroAsync(string codigoCadastro)
        {
            var model = await _alunoRepository.GetByCodigoCadastroAsync(codigoCadastro);
            if (model == null)
            {
                return null;
            }

            var retorno = _mapper.Map<AlunoOutput>(model);

            // Obter matrículas e selecionar a ativa
            var matriculas = await _matriculaService.ListarPorCodigoAluno(model.Codigo);
            retorno.Matricula = matriculas?.FirstOrDefault(x => x.Ativo);

            return retorno;
        }

        public async Task<PagedResult<AlunoOutput>> GetByListarPorFiltroPaginacaoAsync(AlunoInput input)
        {
            var query = await _alunoRepository.GetAllAsync();
            query = query.OrderByDescending(x => x.CodigoCadastro);

            if (input.Ativo.HasValue)
            {
                query = query.Where(x => x.Ativo == input.Ativo.Value);
            }
            if (input.Alergia.HasValue)
            {
                query = query.Where(x => x.Alergia == input.Alergia.Value);
            }
            if (input.RestricaoAlimentar.HasValue)
            {
                query = query.Where(x => x.RestricaoAlimentar == input.RestricaoAlimentar.Value);                
            }
            if (input.DeficienciaOuSituacaoAtipica.HasValue)
            {
                query = query.Where(x => x.DeficienciaOuSituacaoAtipica == input.DeficienciaOuSituacaoAtipica.Value);                
            }
            if (input.Batizado.HasValue)
            {
                query = query.Where(x => x.Batizado == input.Batizado.Value);                
            }
            if (input.Codigo > 0)
            {
                query = query.Where(x => x.Codigo == input.Codigo);
            }
            if (!string.IsNullOrEmpty(input.CodigoCadastro))
            {
                query = query.Where(x => x.CodigoCadastro == input.CodigoCadastro);
            }
            if (!string.IsNullOrEmpty(input.NomeCrianca))
            {
                var nomeFiltro = input.NomeCrianca.RemoverAcentos().ToLower();
                query = query.Where(x => x.NomeCrianca != null &&
                                         x.NomeCrianca.RemoverAcentos().ToLower().Contains(nomeFiltro));
            }
            if (!string.IsNullOrEmpty(input.NomeMae))
            {
                var nomeFiltro = input.NomeMae.RemoverAcentos().ToLower();
                query = query.Where(x => x.NomeMae != null &&
                                         x.NomeMae.RemoverAcentos().ToLower().Contains(nomeFiltro));
            }
            if (!string.IsNullOrEmpty(input.NomePai))
            {
                var nomeFiltro = input.NomePai.RemoverAcentos().ToLower();
                query = query.Where(x => x.NomePai != null &&
                                         x.NomePai.RemoverAcentos().ToLower().Contains(nomeFiltro));
            }
            if (!string.IsNullOrEmpty(input.OutroResponsavel))
            {
                var nomeFiltro = input.OutroResponsavel.RemoverAcentos().ToLower();
                query = query.Where(x => x.OutroResponsavel != null &&
                                         x.OutroResponsavel.RemoverAcentos().ToLower().Contains(nomeFiltro));
            }
            if (input.DataNascimento.HasValue)
            {
                query = query.Where(x => x.DataNascimento != null &&
                                         x.DataNascimento.Value.Date == input.DataNascimento.Value.Date);
            }
            if (input.DataNascimentoInicial.HasValue)
            {
                query = query.Where(x => x.DataNascimento != null && 
                                         x.DataNascimento >= input.DataNascimentoInicial.Value);
            }
            if (input.DataNascimentoFinal.HasValue)
            {
                query = query.Where(x => x.DataNascimento != null && 
                                         x.DataNascimento <= input.DataNascimentoFinal.Value);
            }
            if (input.DataBatizado.HasValue)
            {
                query = query.Where(x => x.DataBatizado != null &&
                                         x.DataBatizado.Value.Date == input.DataBatizado.Value);
            }
            if (input.DataBatizadoInicial.HasValue)
            {
                query = query.Where(x => x.DataBatizado != null &&
                                         x.DataBatizado.Value.Date == input.DataBatizadoInicial.Value);
            }
            if (input.DataBatizadoFinal.HasValue)
            {
                query = query.Where(x => x.DataBatizado != null &&
                                         x.DataBatizado.Value.Date == input.DataBatizadoFinal.Value);
            }
            if (input.DataAtualizacao.HasValue)
            {
                query = query.Where(x => x.DataAtualizacao != null &&
                                         x.DataAtualizacao.Value.Date == input.DataAtualizacao.Value);
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
            if (input.DataCadastro.HasValue)
            {
                query = query.Where(x => x.DataCadastro != null &&
                                         x.DataCadastro.Value.Date == input.DataCadastro.Value);
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
                new GenericComparer<AlunoEntity>(
                    (x, y) => x.Codigo == y.Codigo && x.CodigoCadastro == y.CodigoCadastro,
                    obj => HashCode.Combine(obj.Codigo, obj.CodigoCadastro)
                )
            ).ToList();

            var retorno = _alunoRepository.RetornaPagedResultAsync(listaSemDuplicidade, input.page, input.pageSize, input.isPaginacao);
            var retornoAlunoOutput = new PagedResult<AlunoOutput>()
            {
                Dados = _mapper.Map<List<AlunoOutput>>(retorno.Dados),
                PaginaAtual = retorno.PaginaAtual,
                TamanhoPagina = retorno.TamanhoPagina,
                TotalItens = retorno.TotalItens,
                TotalPaginas = retorno.TotalPaginas
            };

            //por enquanto não retornar a matricula para não sobregarregar o resultado.
            //foreach (var item in retornoAlunoOutput.Dados)
            //{
            //    // Obter matrículas e selecionar a ativa
            //    var matriculas = await _matriculaService.ListarPorCodigoAluno(item.Codigo);
            //    item.Matricula = matriculas?.FirstOrDefault(x => x.Ativo);
            //}

            return retornoAlunoOutput;
        }

        public async Task<int> InsertAsync(AlunoInput entity)
        {
            var model = _mapper.Map<AlunoInput, AlunoEntity>(entity);

            model = _objectExtensionGenerics.TrataCamposNulls(model);
            
            model = TratamentoCamposComunsInsertUpdate(model);
            
            model.DataCadastro = model.DataAtualizacao = DateTime.Now;
            
            var maxCodigoCadastro = await _alunoRepository.GetMaxCodigoCadastroAsync();
            
            model.CodigoCadastro = Convert.ToString(maxCodigoCadastro + 1);

            model.CodigoUsuarioLogado = null;

            return await _alunoRepository.InsertAsync(model);
        }

        public async Task<int> InsertAsync(IEnumerable<AlunoInput> list)
        {
            var models = new List<AlunoEntity>();
            foreach (var item in list)
            {
                var model = _mapper.Map<AlunoInput, AlunoEntity>(item);
                
                model = _objectExtensionGenerics.TrataCamposNulls(model);
                
                model = TratamentoCamposComunsInsertUpdate(model);
                
                model.DataCadastro = model.DataAtualizacao = DateTime.Now;
                
                var maxCodigoCadastro = await _alunoRepository.GetMaxCodigoCadastroAsync();
                
                model.CodigoCadastro = Convert.ToString(maxCodigoCadastro + 1);

                model.CodigoUsuarioLogado = null;

                models.Add(model);
            }

            return await _alunoRepository.InsertAsync(models);
        }

        public async Task<bool> UpdateAsync(AlunoInput entity)
        {
            var model = _mapper.Map<AlunoInput, AlunoEntity>(entity);

            model = _objectExtensionGenerics.TrataCamposNulls(model);

            model = TratamentoCamposComunsInsertUpdate(model);

            model.DataAtualizacao = DateTime.Now;
            
            model.CodigoUsuarioLogado = null;

            return await _alunoRepository.UpdateAsync(model);
        }

        public async Task<bool> UpdateAsync(IEnumerable<AlunoInput> list)
        {
            var models = new List<AlunoEntity>();
            foreach (var item in list)
            {
                var model = _mapper.Map<AlunoInput, AlunoEntity>(item);
                
                model = _objectExtensionGenerics.TrataCamposNulls(model);

                model = TratamentoCamposComunsInsertUpdate(model);

                model.DataAtualizacao = DateTime.Now;

                model.CodigoUsuarioLogado = null;

                models.Add(model);
            }

            return await _alunoRepository.UpdateAsync(models);
        }

        private AlunoEntity TratamentoCamposComunsInsertUpdate(AlunoEntity model)
        {
            if (model.NomeCrianca != null)
            {
                model.NomeCrianca = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.NomeCrianca.ToLowerInvariant());
            }
            if (model.DataNascimento.HasValue)
            {
                model.DataNascimento = model.DataNascimento.GetValueOrDefault().Date;
            }
            if (model.DataBatizado.HasValue)
            {
                model.DataBatizado = model.DataBatizado.GetValueOrDefault().Date;
            }
            if (model.NomeMae != null)
            {
                model.NomeMae = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.NomeMae.ToLowerInvariant());
            }
            if (model.NomePai != null)
            {
                model.NomePai = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.NomePai.ToLowerInvariant());
            }
            if (model.OutroResponsavel != null)
            {
                model.OutroResponsavel = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.OutroResponsavel.ToLowerInvariant());
            }
            if (model.EnderecoEmail != null)
            {
                model.EnderecoEmail = model.EnderecoEmail.ToLowerInvariant();
            }

            return model;
        }
    }
}

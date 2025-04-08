using AutoMapper;
using System.Globalization;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Extensions;
using TRILHAR.Business.Interfaces;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.IO.Aluno;
using TRILHAR.Business.Pagination;


namespace TRILHAR.Business.Services
{
    public class AlunoService : ServiceGenericsBase<AlunoEntity>, IAlunoService
    {
        private readonly IObjectExtensionGenerics<AlunoEntity> _objectExtensionGenerics;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMatriculaRepository _matriculaAlunoTurmaRepository;
        private readonly IVMatriculaRepository _vMatriculaAlunoTurmaRepository;
        private readonly IFrequenciaRepository _frequenciaRepository;
        private readonly IVFrequenciaRepository _vFrequenciaAlunoTurmaRepository;

        public AlunoService(
            INotificador notificador,
            IObjectExtensionGenerics<AlunoEntity> objectExtensionGenerics,
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMatriculaRepository matriculaAlunoTurmaRepository,
            IVMatriculaRepository vMatriculaAlunoTurmaRepository,
            IFrequenciaRepository frequenciaRepository,
            IVFrequenciaRepository vFrequenciaAlunoTurmaRepository,
            IMapper mapper) : base(notificador, mapper, alunoRepository)
        {
            _objectExtensionGenerics = objectExtensionGenerics;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _matriculaAlunoTurmaRepository = matriculaAlunoTurmaRepository;
            _vMatriculaAlunoTurmaRepository = vMatriculaAlunoTurmaRepository;
            _frequenciaRepository = frequenciaRepository;
            _vFrequenciaAlunoTurmaRepository = vFrequenciaAlunoTurmaRepository;
        }

        public async Task<AlunoOutput> GetByCodigoCadastroAsync(string codigoCadastro)
        {
            var model = await _alunoRepository.GetByCodigoCadastroAsync(codigoCadastro);
            
            if (model != null)
            {
                var retorno = _mapper.Map<AlunoEntity, AlunoOutput>(model);
                return retorno;
            }            
            return null;
        }


        public async Task<PagedResult<AlunoOutput>> GetByListarPorFiltroPaginacaoAsync(AlunoInput input, int page = 1, int pageSize = 10, bool isPaginacao = false)
        {
            var listAlunoEntity = new List<AlunoEntity>();
            var listAlunoOutput = new List<AlunoOutput>();
            var parametros = new Dictionary<string, object?>();

            if (input.Codigo.HasValue)
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@Codigo", input.Codigo.Value);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = "Codigo = @Codigo",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }
            if (!string.IsNullOrEmpty(input.NomeCrianca))
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@NomeCrianca", input.NomeCrianca);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = $"LOWER(NomeCrianca) LIKE LOWER('%' + @NomeCrianca + '%')",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }
            if (input.DataNascimento.HasValue)
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@DataNascimento", input.DataNascimento.Value);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = "CONVERT(DATE, DataNascimento) = CONVERT(DATE, @DataNascimento)",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }
            if (input.DataNascimentoInicial.HasValue && input.DataNascimentoFinal.HasValue)
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@DataNascimentoInicial", input.DataNascimentoInicial.Value);
                parametros.Add("@DataNascimentoFinal", input.DataNascimentoFinal.Value);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = "CONVERT(DATE, DataNascimento) BETWEEN CONVERT(DATE, @DataNascimentoInicial) AND CONVERT(DATE, @DataNascimentoFinal)",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }
            if (!string.IsNullOrEmpty(input.NomeMae))
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@NomeMae", input.NomeMae);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = $"LOWER(NomeMae) LIKE LOWER('%' + @NomeMae + '%')",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }
            if (!string.IsNullOrEmpty(input.NomePai))
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@NomePai", input.NomePai);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = $"LOWER(NomePai) LIKE LOWER('%' + @NomePai + '%')",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }
            if (!string.IsNullOrEmpty(input.OutroResponsavel))
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@OutroResponsavel", input.OutroResponsavel);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = $"LOWER(OutroResponsavel) LIKE LOWER('%' + @OutroResponsavel + '%')",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }
            if (input.Alergia.HasValue)
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@Alergia", input.Alergia.Value);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = "Alergia = @Alergia",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }
            if (input.RestricaoAlimentar.HasValue)
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@RestricaoAlimentar", input.RestricaoAlimentar.Value);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = "RestricaoAlimentar = @RestricaoAlimentar",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }
            if (input.DeficienciaOuSituacaoAtipica.HasValue)
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@DeficienciaOuSituacaoAtipica", input.DeficienciaOuSituacaoAtipica.Value);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = "DeficienciaOuSituacaoAtipica = @DeficienciaOuSituacaoAtipica",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }
            if (input.Batizado.HasValue)
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@Batizado", input.Batizado.Value);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = "Batizado = @Batizado",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }
            if (input.DataBatizadoInicial.HasValue && input.DataBatizadoFinal.HasValue)
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@DataBatizadoInicial", input.DataBatizadoInicial.Value);
                parametros.Add("@DataBatizadoFinal", input.DataBatizadoFinal.Value);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = "CONVERT(DATE, DataBatizado) BETWEEN CONVERT(DATE, @DataBatizadoInicial) AND CONVERT(DATE, @DataBatizadoFinal)",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }
            if (input.DataAtualizacaoInicial.HasValue && input.DataAtualizacaoFinal.HasValue)
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@DataAtualizacaoInicial", input.DataAtualizacaoInicial.Value);
                parametros.Add("@DataAtualizacaoFinal", input.DataAtualizacaoFinal.Value);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = "CONVERT(DATE, DataAtualizacao) BETWEEN CONVERT(DATE, @DataAtualizacaoInicial) AND CONVERT(DATE, @DataAtualizacaoFinal)",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }
            if (input.DataCadastroInicial.HasValue && input.DataCadastroFinal.HasValue)
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@DataCadastroInicial", input.DataCadastroInicial.Value);
                parametros.Add("@DataCadastroFinal", input.DataCadastroFinal.Value);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = "CONVERT(DATE, DataCadastro) BETWEEN CONVERT(DATE, @DataCadastroInicial) AND CONVERT(DATE, @DataCadastroFinal)",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }
            if (input.Ativo.HasValue)
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@Ativo", input.Ativo.Value);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = "Ativo = @Ativo",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }
            //nenhuma das opções marcadas. pega todos ativos + inativos
            if (input.Ativo == null && !listAlunoEntity.Any())
            {
                parametros = new Dictionary<string, object?>();
                parametros.Add("@Ativo", true);
                parametros.Add("@Inativo", false);
                var inputCondicaoParametros = new InputCondicaoParametros
                {
                    Condicao = "Ativo = @Ativo OR Ativo = @Inativo",
                    Parametros = parametros
                };

                var temp = await _alunoRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
                if (temp.Any()) listAlunoEntity.AddRange(temp);
            }

            var listaSemDuplicidade = listAlunoEntity.Distinct(
                new GenericComparer<AlunoEntity>(
                    (x, y) => x.Codigo == y.Codigo && x.CodigoCadastro == y.CodigoCadastro,
                    obj => HashCode.Combine(obj.Codigo, obj.CodigoCadastro)
                )
            ).ToList();

            var listaOrdenada = listaSemDuplicidade
                .OrderBy(x => x.NomeCrianca)
                .ToList();

            var retorno = _alunoRepository.RetornaPagedResultAsync(listaSemDuplicidade, page, pageSize, isPaginacao);
            var retornoAlunoOutput = new PagedResult<AlunoOutput>()
            {
                Dados = _mapper.Map<List<AlunoOutput>>(retorno.Dados),
                PaginaAtual = retorno.PaginaAtual,
                TamanhoPagina = retorno.TamanhoPagina,
                TotalItens = retorno.TotalItens,
                TotalPaginas = retorno.TotalPaginas
            };

            return retornoAlunoOutput;
        }



        public async Task<int> InsertAsync(AlunoInput entity)
        {
            var model = _mapper.Map<AlunoInput, AlunoEntity>(entity);

            model = _objectExtensionGenerics.TrataCamposNulls(model);
            
            model = TratamentoCamposComunsInsertUpdat(model);
            
            model.DataCadastro = model.DataAtualizacao = DateTime.Now;
            
            var maxCodigoCadastro = await _alunoRepository.GetMaxCodigoCadastroAsync();
            
            model.CodigoCadastro = Convert.ToString(maxCodigoCadastro + 1);

            return await _alunoRepository.InsertAsync(model);
        }

        public async Task<int> InsertAsync(IEnumerable<AlunoInput> list)
        {
            var models = new List<AlunoEntity>();
            foreach (var item in list)
            {
                var model = _mapper.Map<AlunoInput, AlunoEntity>(item);
                
                model = _objectExtensionGenerics.TrataCamposNulls(model);
                
                model = TratamentoCamposComunsInsertUpdat(model);
                
                model.DataCadastro = model.DataAtualizacao = DateTime.Now;
                
                var maxCodigoCadastro = await _alunoRepository.GetMaxCodigoCadastroAsync();
                
                model.CodigoCadastro = Convert.ToString(maxCodigoCadastro + 1);
                
                models.Add(model);
            }

            return await _alunoRepository.InsertAsync(models);
        }

        public async Task<bool> UpdateAsync(AlunoInput entity)
        {
            var model = _mapper.Map<AlunoInput, AlunoEntity>(entity);

            model = _objectExtensionGenerics.TrataCamposNulls(model);

            model = TratamentoCamposComunsInsertUpdat(model);

            model.DataAtualizacao = DateTime.Now;

            return await _alunoRepository.UpdateAsync(model);
        }

        public async Task<bool> UpdateAsync(IEnumerable<AlunoInput> list)
        {
            var models = new List<AlunoEntity>();
            foreach (var item in list)
            {
                var model = _mapper.Map<AlunoInput, AlunoEntity>(item);
                
                model = _objectExtensionGenerics.TrataCamposNulls(model);

                model = TratamentoCamposComunsInsertUpdat(model);

                model.DataAtualizacao = DateTime.Now;

                models.Add(model);
            }

            return await _alunoRepository.UpdateAsync(models);
        }

        private AlunoEntity TratamentoCamposComunsInsertUpdat(AlunoEntity model)
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

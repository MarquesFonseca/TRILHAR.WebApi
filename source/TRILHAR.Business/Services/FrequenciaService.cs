using AutoMapper;
using System.Data;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.IO.Frequencia;
using TRILHAR.Business.IO.Matricula;

namespace TRILHAR.Business.Services
{
    public class FrequenciaService : ServiceGenericsBase<FrequenciaEntity>, IFrequenciaService
    {
        private readonly IObjectExtensionGenerics<FrequenciaEntity> _objectExtensionGenerics;
        private readonly ICriancaRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMatriculaService _matriculaService;
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IVMatriculaService _vMatriculaService;
        private readonly IVMatriculaRepository _vMatriculaRepository;
        private readonly IFrequenciaRepository _frequenciaRepository;
        private readonly IVFrequenciaRepository _vFrequenciaRepository;
        private readonly IFrequenciasTurmasAgrupadasRepository _frequenciasTurmasAgrupadasRepository;

        public FrequenciaService(
            INotificador notificador,
            IObjectExtensionGenerics<FrequenciaEntity> objectExtensionGenerics,
            ICriancaRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMatriculaService matriculaService,
            IMatriculaRepository matriculaRepository,
            IVMatriculaService vMatriculaService,
            IVMatriculaRepository vMatriculaRepository,
            IFrequenciaRepository frequenciaRepository,
            IVFrequenciaRepository vFrequenciaRepository,
            IFrequenciasTurmasAgrupadasRepository frequenciasTurmasAgrupadasRepository,
            IMapper mapper) : base(notificador, mapper, frequenciaRepository)
        {
            _objectExtensionGenerics = objectExtensionGenerics;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _matriculaService = matriculaService;
            _matriculaRepository = matriculaRepository;
            _vMatriculaService = vMatriculaService;
            _vMatriculaRepository = vMatriculaRepository;
            _frequenciaRepository = frequenciaRepository;
            _vFrequenciaRepository = vFrequenciaRepository;
            _frequenciasTurmasAgrupadasRepository = frequenciasTurmasAgrupadasRepository;
            _vMatriculaService = vMatriculaService;
        }

        public async Task<int> AddAsync(FrequenciaInput input)
        {
            var model = _mapper.Map<FrequenciaInput, FrequenciaEntity>(input);

            model = _objectExtensionGenerics.TrataCamposNulls(model);
            model.CodigoUsuarioLogado = null;
            model.DataCadastro =
            model.DataAtualizacao =
            model.DataFrequencia = DateTime.Now;

            return await _frequenciaRepository.InsertAsync(model);
        }

        public async Task<int> AddManyAsync(IEnumerable<FrequenciaInput> inputs)
        {
            var models = new List<FrequenciaEntity>();
            foreach (var item in inputs)
            {
                var model = _mapper.Map<FrequenciaInput, FrequenciaEntity>(item);
                model = _objectExtensionGenerics.TrataCamposNulls(model);
                model.CodigoUsuarioLogado = null;
                model.DataCadastro =
                model.DataAtualizacao =
                model.DataFrequencia = DateTime.Now;
                models.Add(model);
            }

            return await _frequenciaRepository.InsertAsync(models);
        }

        public async Task<bool> UpdateAsync(FrequenciaInput input)
        {
            var model = _mapper.Map<FrequenciaInput, FrequenciaEntity>(input);

            model = _objectExtensionGenerics.TrataCamposNulls(model);
            
            model.DataAtualizacao = DateTime.Now;
            
            return await _frequenciaRepository.UpdateAsync(model);
        }

        public async Task<bool> UpdateManyAsync(IEnumerable<FrequenciaInput> inputs)
        {
            var models = new List<FrequenciaEntity>();
            foreach (var item in inputs)
            {
                var model = _mapper.Map<FrequenciaInput, FrequenciaEntity>(item);
                
                model = _objectExtensionGenerics.TrataCamposNulls(model);
                
                model.DataAtualizacao = DateTime.Now;
                
                models.Add(model);
            }

            return await _frequenciaRepository.UpdateAsync(models);
        }

        //3
        public async Task<IEnumerable<VFrequenciaOutput>?> GetByDateAsync(DateTime dataFrequencia)
        {
            var inputCondicaoParametros = new InputConsultaPersonalizada
            {
                ConsultaPersonalizada = "SPFrequenciasPorData @DataFrequencia",
                Condicao = null,
                Parametros = new Dictionary<string, object?>
                {
                    { "@DataFrequencia", dataFrequencia.Date }
                },
                CommandType = CommandType.StoredProcedure
            };

            var listaFrequeciasTemp = await _vFrequenciaRepository.QueryDynamicSql(inputCondicaoParametros);
            var listaFrequenciasPresentes = listaFrequeciasTemp.Where(x => x.Presenca.Equals(true));
            var listaFrequenciasAusentes = await RetornaListaAusentesByAndDataAsync(dataFrequencia, listaFrequenciasPresentes);

            var listaFrequecias = new List<VFrequenciaEntity>();
            listaFrequecias.AddRange(listaFrequenciasPresentes);
            listaFrequecias.AddRange(listaFrequenciasAusentes);
            listaFrequecias
                .OrderByDescending(freq => freq.DataFrequencia)
                .OrderByDescending(turma => turma.TurmaIdadeInicialAluno)
                .ThenBy(turma => turma.TurmaSemestreLetivo)
                .ThenBy(turma => turma.TurmaAnoLetivo)
                .ThenBy(aluno => aluno.AlunoNomeCrianca);
            var resultado = _mapper.Map<IEnumerable<VFrequenciaOutput>>(listaFrequecias);

            return resultado.Any() ? resultado : null;
        }

        //4
        public async Task<IEnumerable<FrequenciasTurmasAgrupadasOutput>?> GetTurmasAgrupadasByDateAsync(DateTime dataFrequencia)
        {
            var inputCondicaoParametros = new InputConsultaPersonalizada
            {
                ConsultaPersonalizada = "SPFrequenciasTodasTurmasAgrupadasDia @DataFrequencia",
                Condicao = null,
                Parametros = new Dictionary<string, object?>
                {
                    { "@DataFrequencia", dataFrequencia.Date }
                },
                CommandType = CommandType.StoredProcedure
            };

            var listaFrequecias = await _frequenciasTurmasAgrupadasRepository.QueryDynamicSql(inputCondicaoParametros);

            var retorno = listaFrequecias.Select(x => new FrequenciasTurmasAgrupadasOutput
            {
                //FrequenciasTurmasAgrupadasEntity
                DataFrequencia = x.DataFrequencia,
                CodigoTurma = x.CodigoTurma,
                TurmaDescricao = x.TurmaDescricao,
                TurmaAnoLetivo = x.TurmaAnoLetivo,
                TurmaSemestreLetivo = x.TurmaSemestreLetivo,
                TurmaIdadeInicialAluno = x.TurmaIdadeInicialAluno,
                TurmaIdadeFinalAluno = x.TurmaIdadeFinalAluno,
                TurmaAtivo = x.TurmaAtivo,
                TurmaLimiteMaximo = x.TurmaLimiteMaximo,
                Qtd = x.Qtd,

                //FrequenciasTurmasAgrupadasOutput
                DataFrequenciaFormatada = x.DataFrequencia.ToShortDateString(),
                TurmaDescricaoFormatada = $"{x.TurmaDescricao} - {x.TurmaAnoLetivo}/{x.TurmaSemestreLetivo}",
                TurmaIdadeInicialAlunoFormatada = x.TurmaIdadeInicialAluno.ToShortDateString(),
                TurmaIdadeFinalAlunoFormatada = x.TurmaIdadeFinalAluno.ToShortDateString(),
                QtdRestante = RetornaQtdRestante(x.Qtd, x.TurmaLimiteMaximo),//x.TurmaLimiteMaximo - x.Qtd,
                QtdRestanteFormatada = RetornaQtdRestanteFormatada(x.Qtd, x.TurmaLimiteMaximo),
            });

            return retorno;
        }

        //5
        public async Task<IEnumerable<VFrequenciaOutput>?> GetByTurmasAndDateAsync(int codigoTurma, DateTime dataFrequencia)
        {
            var dataFormatada = string.Format("{0:D4}-{1:D2}-{2:D2}", dataFrequencia.Date.Year, dataFrequencia.Date.Month, dataFrequencia.Date.Day);
            var inputCondicaoParametros = new InputCondicaoParametros
            {
                Condicao = $"CodigoTurma = @CodigoTurma " +
                $"AND CONVERT(DATE, DataFrequencia) = CONVERT(DATE, '{dataFormatada}') " +
                "AND Presenca = @Presenca",
                Parametros = new Dictionary<string, object?>
                {
                    { "@CodigoTurma", codigoTurma },
                    //{ "@DataFrequencia", dataFrequencia.ToString("yyyy-MM-dd") },
                    //{ "@DataFrequencia", string.Format("{0}-{1}-{2]", dataFrequencia.Date.Year, dataFrequencia.Date.Month, dataFrequencia.Date.Day ) },
                    { "@Presenca", Convert.ToBoolean(1) }
                }
            };

            var listaFrequenciasPresentes = await _vFrequenciaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
            
            var listaFrequenciasAusentes = await RetornaListaAusentesByCodigoTurmaAndDataAsync(codigoTurma, dataFrequencia, listaFrequenciasPresentes);

            var listaFrequecias = new List<VFrequenciaEntity>();
            listaFrequecias.AddRange(listaFrequenciasPresentes);
            listaFrequecias.AddRange(listaFrequenciasAusentes);
            listaFrequecias
                .OrderByDescending(freq => freq.DataFrequencia)
                .OrderByDescending(turma => turma.TurmaIdadeInicialAluno)
                .ThenBy(turma => turma.TurmaSemestreLetivo)
                .ThenBy(turma => turma.TurmaAnoLetivo)
                .ThenBy(aluno => aluno.AlunoNomeCrianca);
            var resultado = _mapper.Map<IEnumerable<VFrequenciaOutput>>(listaFrequecias);

            return resultado.Any() ? resultado : null;
        }        

        //6
        public async Task<IEnumerable<VFrequenciaOutput>?> GetByAlunoAsync(int codigoAluno)
        {
            var inputCondicaoParametros = new InputCondicaoParametros
            {
                Condicao = "CodigoAluno = @CodigoAluno",
                Parametros = new Dictionary<string, object?>
                {
                    { "@CodigoAluno", codigoAluno }
                }
            };

            var listaFrequecias = await _vFrequenciaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
            listaFrequecias
                .OrderByDescending(freq => freq.DataFrequencia)
                .OrderByDescending(turma => turma.TurmaIdadeInicialAluno)
                .ThenBy(turma => turma.TurmaSemestreLetivo)
                .ThenBy(turma => turma.TurmaAnoLetivo)
                .ThenBy(aluno => aluno.AlunoNomeCrianca);
            var resultado = _mapper.Map<IEnumerable<VFrequenciaOutput>>(listaFrequecias);

            return resultado.Any() ? resultado : null;
        }

        //7
        public async Task<IEnumerable<VFrequenciaOutput>?> GetByTurmaAsync(int codigoTurma)
        {
            var inputCondicaoParametros = new InputCondicaoParametros
            {
                Condicao = "CodigoTurma = @CodigoTurma AND Presenca = @Presenca",
                Parametros = new Dictionary<string, object?>
                {
                    { "@CodigoTurma", codigoTurma },
                    { "@Presenca", true }
                }
            };

            var listaFrequecias = await _vFrequenciaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
            listaFrequecias
                .OrderByDescending(freq => freq.DataFrequencia)
                .OrderByDescending(turma => turma.TurmaIdadeInicialAluno)
                .ThenBy(turma => turma.TurmaSemestreLetivo)
                .ThenBy(turma => turma.TurmaAnoLetivo)
                .ThenBy(aluno => aluno.AlunoNomeCrianca);
            var resultado = _mapper.Map<IEnumerable<VFrequenciaOutput>>(listaFrequecias);

            return resultado.Any() ? resultado : null;
        }
        
        //8
        public async Task<IEnumerable<VFrequenciaOutput>?> GetByAlunoAndTurmaAsync(int codigoAluno, int codigoTurma)
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

            var listaFrequecias = await _vFrequenciaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);
            listaFrequecias
                .OrderByDescending(freq => freq.DataFrequencia)
                .OrderByDescending(turma => turma.TurmaIdadeInicialAluno)
                .ThenBy(turma => turma.TurmaSemestreLetivo)
                .ThenBy(turma => turma.TurmaAnoLetivo)
                .ThenBy(aluno => aluno.AlunoNomeCrianca);
            var resultado = _mapper.Map<IEnumerable<VFrequenciaOutput>>(listaFrequecias);

            return resultado.Any() ? resultado : null;
        }

        public async Task<List<VFrequenciaEntity>> RetornaListaAusentesByAndDataAsync(DateTime dataFrequencia, IEnumerable<VFrequenciaEntity> listaFrequeciasPresentes)
        {
            var listaFrequenciasAusentes = new List<VFrequenciaEntity>();

            foreach (var item in listaFrequeciasPresentes.GroupBy(x => x.CodigoTurma))
            {
                int codigoTurma = item.Key;
                var listaAusentesTurma = await RetornaListaAusentesByCodigoTurmaAndDataAsync(codigoTurma, dataFrequencia, listaFrequeciasPresentes);
                listaFrequenciasAusentes.AddRange(listaAusentesTurma);
            }

            return listaFrequenciasAusentes.Any() ? listaFrequenciasAusentes : new List<VFrequenciaEntity>();
        }
        
        public async Task<List<VFrequenciaEntity>> RetornaListaAusentesByCodigoTurmaAndDataAsync(int codigoTurma, DateTime dataFrequencia, IEnumerable<VFrequenciaEntity> listaFrequeciasPresentes)
        {
            var listaFrequenciasAusentes = new List<VFrequenciaEntity>();

            var matriculasAtivasTurma = await _vMatriculaService.ListarPorCodigoTurma(codigoTurma);
            if (matriculasAtivasTurma != null && matriculasAtivasTurma.Any())
            {
                foreach (var item in matriculasAtivasTurma.Where(x => x.Ativo))
                {
                    if (!listaFrequeciasPresentes.Any(x => x.CodigoAluno == item.CodigoAluno && x.CodigoTurma == item.CodigoTurma && x.Presenca == true))
                    {
                        listaFrequenciasAusentes.Add(new VFrequenciaEntity()
                        {
                            //Codigo = 0, 
                            DataFrequencia = dataFrequencia.Date,
                            CodigoAluno = item.CodigoAluno,
                            CodigoTurma = item.CodigoTurma,
                            Presenca = false,
                            CodigoUsuarioLogado = 0,
                            //DataAtualizacao, 
                            //DataCadastro, 
                            AlunoCodigoCadastro = item.AlunoCodigoCadastro,
                            AlunoNomeCrianca = item.AlunoNomeCrianca,
                            AlunoDataNascimento = item.AlunoDataNascimento,
                            AlunoNomeMae = item.AlunoNomeMae,
                            AlunoNomePai = item.AlunoNomePai,
                            AlunoOutroResponsavel = item.AlunoOutroResponsavel,
                            AlunoTelefone = item.AlunoTelefone,
                            AlunoEnderecoEmail = item.AlunoEnderecoEmail,
                            AlunoAlergia = item.AlunoAlergia,
                            AlunoDescricaoAlergia = item.AlunoDescricaoAlergia,
                            AlunoRestricaoAlimentar = item.AlunoRestricaoAlimentar,
                            AlunoDescricaoRestricaoAlimentar = item.AlunoDescricaoRestricaoAlimentar,
                            AlunoDeficienciaOuSituacaoAtipica = item.AlunoDeficienciaOuSituacaoAtipica,
                            AlunoDescricaoDeficiencia = item.AlunoDescricaoDeficiencia,
                            AlunoBatizado = item.AlunoBatizado,
                            AlunoDataBatizado = item.AlunoDataBatizado,
                            AlunoIgrejaBatizado = item.AlunoIgrejaBatizado,
                            AlunoAtivo = item.AlunoAtivo,
                            TurmaDescricao = item.TurmaDescricao,
                            TurmaIdadeInicialAluno = item.TurmaIdadeInicialAluno,
                            TurmaIdadeFinalAluno = item.TurmaIdadeFinalAluno,
                            TurmaAnoLetivo = item.TurmaAnoLetivo,
                            TurmaSemestreLetivo = item.TurmaSemestreLetivo,
                            //TurmaLimiteMaximo = ,
                            TurmaAtivo = item.TurmaAtivo
                        });
                    }
                }
            }

            return listaFrequenciasAusentes.Any() ? listaFrequenciasAusentes : new List<VFrequenciaEntity>();
        }

        private int RetornaQtdRestante(int qtd, int turmaLimiteMaximo)
        {
            //x.TurmaLimiteMaximo - x.Qtd
            return qtd - turmaLimiteMaximo;
            //if (qtd > turmaLimiteMaximo)
            //{
            //    int qtdExcedente = qtd - turmaLimiteMaximo;
            //    return qtdExcedente;
            //}
            //if (qtd < turmaLimiteMaximo)
            //{
            //    int qtdRestante = turmaLimiteMaximo - qtd;
            //    return qtdRestante;
            //}
            //return 0;
        }

        private string RetornaQtdRestanteFormatada(int qtd, int turmaLimiteMaximo)
        {
            if (qtd > turmaLimiteMaximo)
            {
                int qtdExcedente = qtd - turmaLimiteMaximo;
                return $"+{qtdExcedente}";
            }
            if (qtd < turmaLimiteMaximo)
            {
                int qtdRestante = turmaLimiteMaximo - qtd;
                return $"-{qtdRestante}";
            }
            return "=";
        }

    }
}

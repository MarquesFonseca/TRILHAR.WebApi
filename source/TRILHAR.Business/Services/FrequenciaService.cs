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
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IVMatriculaRepository _vMatriculaRepository;
        private readonly IFrequenciaRepository _frequenciaRepository;
        private readonly IVFrequenciaRepository _vFrequenciaRepository;
        private readonly IFrequenciasTurmasAgrupadasRepository _frequenciasTurmasAgrupadasRepository;

        public FrequenciaService(
            INotificador notificador,
            IObjectExtensionGenerics<FrequenciaEntity> objectExtensionGenerics,
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMatriculaRepository matriculaRepository,
            IVMatriculaRepository vMatriculaRepository,
            IFrequenciaRepository frequenciaRepository,
            IVFrequenciaRepository vFrequenciaRepository,
            IFrequenciasTurmasAgrupadasRepository frequenciasTurmasAgrupadasRepository,
            IMapper mapper) : base(notificador, mapper, frequenciaRepository)
        {
            _objectExtensionGenerics = objectExtensionGenerics;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _matriculaRepository = matriculaRepository;
            _vMatriculaRepository = vMatriculaRepository;
            _frequenciaRepository = frequenciaRepository;
            _vFrequenciaRepository = vFrequenciaRepository;
            _frequenciasTurmasAgrupadasRepository = frequenciasTurmasAgrupadasRepository;
        }

        public async Task<int> AddAsync(FrequenciaInput input)
        {
            var model = _mapper.Map<FrequenciaInput, FrequenciaEntity>(input);

            model = _objectExtensionGenerics.TrataCamposNulls(model);
            model.CodigoUsuarioLogado = null;
            model.DataCadastro = model.DataAtualizacao = DateTime.Now;

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
                model.DataCadastro = model.DataAtualizacao = DateTime.Now;
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

        public async Task<IEnumerable<FrequenciaOutput>?> GetByAlunoAsync(int codigoAluno)
        {
            var inputCondicaoParametros = new InputCondicaoParametros
            {
                Condicao = "CodigoAluno = @CodigoAluno",
                Parametros = new Dictionary<string, object?>
                {
                    { "@CodigoAluno", codigoAluno }
                }
            };

            var listaFrequecias = await _frequenciaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);

            var resultado = _mapper.Map<IEnumerable<FrequenciaOutput>>(listaFrequecias);

            return resultado.Any() ? resultado : null;
        }

        public async Task<IEnumerable<FrequenciaOutput>?> GetByTurmaAsync(int codigoTurma)
        {
            var inputCondicaoParametros = new InputCondicaoParametros
            {
                Condicao = "CodigoTurma = @CodigoTurma",
                Parametros = new Dictionary<string, object?>
                {
                    { "@CodigoTurma", codigoTurma }
                }
            };

            var listaFrequecias = await _frequenciaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);

            var resultado = _mapper.Map<IEnumerable<FrequenciaOutput>>(listaFrequecias);

            return resultado.Any() ? resultado : null;
        }

        public async Task<IEnumerable<FrequenciaOutput>?> GetByAlunoAndTurmaAsync(int codigoAluno, int codigoTurma)
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

            var listaFrequecias = await _frequenciaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);

            var resultado = _mapper.Map<IEnumerable<FrequenciaOutput>>(listaFrequecias);

            return resultado.Any() ? resultado : null;
        }

        public async Task<IEnumerable<dynamic>?> GetAlunosByDateAsync(DateTime dataFrequencia)
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

            var listaFrequecias = await _vFrequenciaRepository.QueryDynamicSql(inputCondicaoParametros);
            return listaFrequecias;
        }

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

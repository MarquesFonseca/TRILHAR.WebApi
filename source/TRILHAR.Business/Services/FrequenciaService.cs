using AutoMapper;
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

        public FrequenciaService(
            INotificador notificador,
            IObjectExtensionGenerics<FrequenciaEntity> objectExtensionGenerics,
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMatriculaRepository matriculaRepository,
            IVMatriculaRepository vMatriculaRepository,
            IFrequenciaRepository frequenciaRepository,
            IVFrequenciaRepository vFrequenciaRepository,
            IMapper mapper) : base(notificador, mapper, frequenciaRepository)
        {
            _objectExtensionGenerics = objectExtensionGenerics;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _matriculaRepository = matriculaRepository;
            _vMatriculaRepository = vMatriculaRepository;
            _frequenciaRepository = frequenciaRepository;
            _vFrequenciaRepository = vFrequenciaRepository;
        }

        public async Task<IEnumerable<FrequenciaOutput>> ListarPorCodigoAlunoCodigoTurma(int codigoAluno, int codigoTurma)
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

            var temp = await _frequenciaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);

            var resultado = _mapper.Map<IEnumerable<FrequenciaOutput>>(temp);
            return resultado;
        }

        public async Task<IEnumerable<FrequenciaOutput>> ListarPorCodigoAluno(int codigoAluno)
        {
            var inputCondicaoParametros = new InputCondicaoParametros
            {
                Condicao = "CodigoAluno = @CodigoAluno",
                Parametros = new Dictionary<string, object?>
                {
                    { "@CodigoAluno", codigoAluno }
                }
            };

            var temp = await _frequenciaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);

            if (temp.Any())
            {
                var resultado = _mapper.Map<IEnumerable<FrequenciaOutput>>(temp);
                return resultado;
            }

            return (IEnumerable<FrequenciaOutput>)(temp);
        }

        public async Task<IEnumerable<FrequenciaOutput>> ListarPorCodigoTurma(int codigoTurma)
        {
            var inputCondicaoParametros = new InputCondicaoParametros
            {
                Condicao = "CodigoTurma = @CodigoTurma",
                Parametros = new Dictionary<string, object?>
                {
                    { "@CodigoTurma", codigoTurma }
                }
            };

            var temp = await _frequenciaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);

            if (temp.Any())
            {
                var resultado = _mapper.Map<IEnumerable<FrequenciaOutput>>(temp);
                return resultado;
            }

            return (IEnumerable<FrequenciaOutput>)(temp);
        }

        public async Task<int> InsertAsync(FrequenciaInput entity)
        {
            var model = _mapper.Map<FrequenciaInput, FrequenciaEntity>(entity);

            model = _objectExtensionGenerics.TrataCamposNulls(model);
            model.CodigoUsuarioLogado = null;
            model.DataCadastro = model.DataAtualizacao = DateTime.Now;

            return await _frequenciaRepository.InsertAsync(model);
        }

        public async Task<int> InsertAsync(IEnumerable<FrequenciaInput> list)
        {
            var models = new List<FrequenciaEntity>();
            foreach (var item in list)
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

        public async Task<bool> UpdateAsync(IEnumerable<FrequenciaInput> list)
        {
            var models = new List<FrequenciaEntity>();
            foreach (var item in list)
            {
                var model = _mapper.Map<FrequenciaInput, FrequenciaEntity>(item);
                model = _objectExtensionGenerics.TrataCamposNulls(model);
                model.DataAtualizacao = DateTime.Now;
                models.Add(model);
            }

            return await _frequenciaRepository.UpdateAsync(models);
        }
    }
}

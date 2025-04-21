using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.IO.Aluno;
using TRILHAR.Business.IO.Matricula;
using TRILHAR.Business.IO.Turma;

namespace TRILHAR.Business.Services
{
    public class MatriculaService : ServiceGenericsBase<MatriculaEntity>, IMatriculaService
    {
        private readonly IObjectExtensionGenerics<MatriculaEntity> _objectExtensionGenerics;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IVMatriculaRepository _vMatriculaRepository;
        private readonly IFrequenciaRepository _frequenciaRepository;
        private readonly IVFrequenciaRepository _vFrequenciaRepository;

        public MatriculaService(
            INotificador notificador,
            IObjectExtensionGenerics<MatriculaEntity> objectExtensionGenerics,
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMatriculaRepository matriculaRepository,
            IVMatriculaRepository vMatriculaRepository,
            IFrequenciaRepository frequenciaRepository,
            IVFrequenciaRepository vFrequenciaRepository,
            IMapper mapper) : base(notificador, mapper, matriculaRepository)
        {
            _objectExtensionGenerics = objectExtensionGenerics;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _matriculaRepository = matriculaRepository;
            _vMatriculaRepository = vMatriculaRepository;
            _frequenciaRepository = frequenciaRepository;
            _vFrequenciaRepository = vFrequenciaRepository;
        }

        public async Task<IEnumerable<MatriculaOutput>?> ListarPorCodigoAlunoCodigoTurma(int codigoAluno, int codigoTurma)
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

            var listaMatriculas = await _matriculaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);

            var resultado = _mapper.Map<IEnumerable<MatriculaOutput>>(listaMatriculas);

            return resultado.Any() ? resultado : null;
        }

        public async Task<IEnumerable<MatriculaOutput>?> ListarPorCodigoAluno(int codigoAluno)
        {
            var inputCondicaoParametros = new InputCondicaoParametros
            {
                Condicao = "CodigoAluno = @CodigoAluno",
                Parametros = new Dictionary<string, object?>
                {
                    { "@CodigoAluno", codigoAluno }
                }
            };

            var listaMatriculas = await _matriculaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);

            var resultado = _mapper.Map<IEnumerable<MatriculaOutput>>(listaMatriculas);

            return resultado.Any() ? resultado : null;
        }

        public async Task<IEnumerable<MatriculaOutput>?> ListarPorCodigoTurma(int codigoTurma)
        {
            var inputCondicaoParametros = new InputCondicaoParametros
            {
                Condicao = "CodigoTurma = @CodigoTurma",
                Parametros = new Dictionary<string, object?>
                {
                    { "@CodigoTurma", codigoTurma }
                }
            };

            var listaMatriculas = await _matriculaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);

            var resultado = _mapper.Map<IEnumerable<MatriculaOutput>>(listaMatriculas);

            return resultado.Any() ? resultado : null;
        }

        public async Task<int> InsertAsync(MatriculaInput entity)
        {
            var model = _mapper.Map<MatriculaInput, MatriculaEntity>(entity);

            model = _objectExtensionGenerics.TrataCamposNulls(model);
            model.CodigoUsuarioLogado = null;
            model.DataCadastro = model.DataAtualizacao = DateTime.Now;

            return await _matriculaRepository.InsertAsync(model);
        }

        public async Task<int> InsertAsync(IEnumerable<MatriculaInput> list)
        {
            var models = new List<MatriculaEntity>();
            foreach (var item in list)
            {
                var model = _mapper.Map<MatriculaInput, MatriculaEntity>(item);
                model = _objectExtensionGenerics.TrataCamposNulls(model);
                model.CodigoUsuarioLogado = null;
                model.DataCadastro = model.DataAtualizacao = DateTime.Now;
                models.Add(model);
            }

            return await _matriculaRepository.InsertAsync(models);
        }

        public async Task<bool> UpdateAsync(MatriculaInput input)
        {
            var model = _mapper.Map<MatriculaInput, MatriculaEntity>(input);

            model = _objectExtensionGenerics.TrataCamposNulls(model);
            model.DataAtualizacao = DateTime.Now;
            return await _matriculaRepository.UpdateAsync(model);
        }

        public async Task<bool> UpdateAsync(IEnumerable<MatriculaInput> list)
        {
            var models = new List<MatriculaEntity>();
            foreach (var item in list)
            {
                var model = _mapper.Map<MatriculaInput, MatriculaEntity>(item);
                model = _objectExtensionGenerics.TrataCamposNulls(model);
                model.DataAtualizacao = DateTime.Now;
                models.Add(model);
            }

            return await _matriculaRepository.UpdateAsync(models);
        }
    }
}

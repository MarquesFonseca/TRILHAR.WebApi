using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.IO.Aluno;
using TRILHAR.Business.IO.Matricula;

namespace TRILHAR.Business.Services
{
    public class MatriculaAlunoTurmaService : ServiceGenericsBase<MatriculaAlunoTurmaEntity>, IMatriculaAlunoTurmaService
    {
        private readonly IObjectExtensionGenerics<MatriculaAlunoTurmaEntity> _objectExtensionGenerics;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMatriculaAlunoTurmaRepository _matriculaAlunoTurmaRepository;
        private readonly IVMatriculaAlunoTurmaRepository _vMatriculaAlunoTurmaRepository;
        private readonly IFrequenciaRepository _frequenciaRepository;
        private readonly IVFrequenciaAlunoTurmaRepository _vFrequenciaAlunoTurmaRepository;
        private readonly IMapper _mapper;

        public MatriculaAlunoTurmaService(
            INotificador notificador,
            IObjectExtensionGenerics<MatriculaAlunoTurmaEntity> objectExtensionGenerics,
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMatriculaAlunoTurmaRepository matriculaAlunoTurmaRepository,
            IVMatriculaAlunoTurmaRepository vMatriculaAlunoTurmaRepository,
            IFrequenciaRepository frequenciaRepository,
            IVFrequenciaAlunoTurmaRepository vFrequenciaAlunoTurmaRepository,
            IMapper mapper) : base(notificador, matriculaAlunoTurmaRepository)
        {
            _objectExtensionGenerics = objectExtensionGenerics;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _matriculaAlunoTurmaRepository = matriculaAlunoTurmaRepository;
            _vMatriculaAlunoTurmaRepository = vMatriculaAlunoTurmaRepository;
            _frequenciaRepository = frequenciaRepository;
            _vFrequenciaAlunoTurmaRepository = vFrequenciaAlunoTurmaRepository;
            _mapper = mapper;
        }

        public async Task<int> InsertAsync(MatriculaInput entity)
        {
            var model = _mapper.Map<MatriculaInput, MatriculaAlunoTurmaEntity>(entity);

            model = _objectExtensionGenerics.TrataCamposNulls(model);
            model.CodigoUsuarioLogado = null;
            model.DataCadastro = model.DataAtualizacao = DateTime.Now;

            return await _matriculaAlunoTurmaRepository.InsertAsync(model);
        }

        public async Task<int> InsertAsync(IEnumerable<MatriculaInput> list)
        {
            var models = new List<MatriculaAlunoTurmaEntity>();
            foreach (var item in list)
            {
                var model = _mapper.Map<MatriculaInput, MatriculaAlunoTurmaEntity>(item);
                model = _objectExtensionGenerics.TrataCamposNulls(model);
                model.CodigoUsuarioLogado = null;
                model.DataCadastro = model.DataAtualizacao = DateTime.Now;
                models.Add(model);
            }

            return await _matriculaAlunoTurmaRepository.InsertAsync(models);
        }

        public async Task<bool> UpdateAsync(MatriculaInput entity)
        {
            var model = _mapper.Map<MatriculaInput, MatriculaAlunoTurmaEntity>(entity);

            model = _objectExtensionGenerics.TrataCamposNulls(model);
            model.DataAtualizacao = DateTime.Now;
            return await _matriculaAlunoTurmaRepository.UpdateAsync(model);
        }

        public async Task<bool> UpdateAsync(IEnumerable<MatriculaInput> list)
        {
            var models = new List<MatriculaAlunoTurmaEntity>();
            foreach (var item in list)
            {
                var model = _mapper.Map<MatriculaInput, MatriculaAlunoTurmaEntity>(item);
                model = _objectExtensionGenerics.TrataCamposNulls(model);
                model.DataAtualizacao = DateTime.Now;
                models.Add(model);
            }

            return await _matriculaAlunoTurmaRepository.UpdateAsync(models);
        }
    }
}

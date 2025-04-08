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
    public class MatriculaService : ServiceGenericsBase<MatriculaEntity>, IMatriculaService
    {
        private readonly IObjectExtensionGenerics<MatriculaEntity> _objectExtensionGenerics;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMatriculaRepository _matriculaAlunoTurmaRepository;
        private readonly IVMatriculaRepository _vMatriculaAlunoTurmaRepository;
        private readonly IFrequenciaRepository _frequenciaRepository;
        private readonly IVFrequenciaRepository _vFrequenciaAlunoTurmaRepository;

        public MatriculaService(
            INotificador notificador,
            IObjectExtensionGenerics<MatriculaEntity> objectExtensionGenerics,
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMatriculaRepository matriculaAlunoTurmaRepository,
            IVMatriculaRepository vMatriculaAlunoTurmaRepository,
            IFrequenciaRepository frequenciaRepository,
            IVFrequenciaRepository vFrequenciaAlunoTurmaRepository,
            IMapper mapper) : base(notificador, mapper, matriculaAlunoTurmaRepository)
        {
            _objectExtensionGenerics = objectExtensionGenerics;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _matriculaAlunoTurmaRepository = matriculaAlunoTurmaRepository;
            _vMatriculaAlunoTurmaRepository = vMatriculaAlunoTurmaRepository;
            _frequenciaRepository = frequenciaRepository;
            _vFrequenciaAlunoTurmaRepository = vFrequenciaAlunoTurmaRepository;
        }

        public async Task<int> InsertAsync(MatriculaInput entity)
        {
            var model = _mapper.Map<MatriculaInput, MatriculaEntity>(entity);

            model = _objectExtensionGenerics.TrataCamposNulls(model);
            model.CodigoUsuarioLogado = null;
            model.DataCadastro = model.DataAtualizacao = DateTime.Now;

            return await _matriculaAlunoTurmaRepository.InsertAsync(model);
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

            return await _matriculaAlunoTurmaRepository.InsertAsync(models);
        }

        public async Task<bool> UpdateAsync(MatriculaInput entity)
        {
            var model = _mapper.Map<MatriculaInput, MatriculaEntity>(entity);

            model = _objectExtensionGenerics.TrataCamposNulls(model);
            model.DataAtualizacao = DateTime.Now;
            return await _matriculaAlunoTurmaRepository.UpdateAsync(model);
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

            return await _matriculaAlunoTurmaRepository.UpdateAsync(models);
        }
    }
}

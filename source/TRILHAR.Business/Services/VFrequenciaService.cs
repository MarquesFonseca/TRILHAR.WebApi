using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;

namespace TRILHAR.Business.Services
{
    public class VFrequenciaService : ServiceGenericsBase<VFrequenciaEntity>, IVFrequenciaService
    {
        private readonly IObjectExtensionGenerics<VFrequenciaEntity> _objectExtensionGenerics;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IVMatriculaRepository _vMatriculaRepository;
        private readonly IFrequenciaRepository _frequenciaRepository;
        private readonly IVFrequenciaRepository _vFrequenciaRepository;

        public VFrequenciaService(
            INotificador notificador,
            IObjectExtensionGenerics<VFrequenciaEntity> objectExtensionGenerics,
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMatriculaRepository matriculaRepository,
            IVMatriculaRepository vMatriculaRepository,
            IFrequenciaRepository frequenciaRepository,
            IVFrequenciaRepository vFrequenciaRepository,
            IMapper mapper) : base(notificador, mapper, vFrequenciaRepository)
        {
            _objectExtensionGenerics = objectExtensionGenerics;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _matriculaRepository = matriculaRepository;
            _vMatriculaRepository = vMatriculaRepository;
            _frequenciaRepository = frequenciaRepository;
            _vFrequenciaRepository = vFrequenciaRepository;
        }
    }
}

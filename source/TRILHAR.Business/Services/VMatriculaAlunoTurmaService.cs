using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;

namespace TRILHAR.Business.Services
{
    public class VMatriculaAlunoTurmaService : ServiceGenericsBase<VMatriculaAlunoTurmaEntity>, IVMatriculaAlunoTurmaService
    {
        private readonly IObjectExtensionGenerics<VMatriculaAlunoTurmaEntity> _objectExtensionGenerics;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMatriculaAlunoTurmaRepository _matriculaAlunoTurmaRepository;
        private readonly IVMatriculaAlunoTurmaRepository _vMatriculaAlunoTurmaRepository;
        private readonly IFrequenciaRepository _frequenciaRepository;
        private readonly IVFrequenciaAlunoTurmaRepository _vFrequenciaAlunoTurmaRepository;
        private readonly IMapper _mapper;

        public VMatriculaAlunoTurmaService(
            INotificador notificador,
            IObjectExtensionGenerics<VMatriculaAlunoTurmaEntity> objectExtensionGenerics,
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMatriculaAlunoTurmaRepository matriculaAlunoTurmaRepository,
            IVMatriculaAlunoTurmaRepository vMatriculaAlunoTurmaRepository,
            IFrequenciaRepository frequenciaRepository,
            IVFrequenciaAlunoTurmaRepository vFrequenciaAlunoTurmaRepository,
            IMapper mapper) : base(notificador, vMatriculaAlunoTurmaRepository)
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
    }
}

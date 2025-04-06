using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.IO.Turma;

namespace TRILHAR.Business.Services
{
    public class TurmaService : ServiceGenericsBase<TurmaEntity>, ITurmaService
    {
        private readonly IObjectExtensionGenerics<TurmaEntity> _objectExtensionGenerics;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMatriculaAlunoTurmaRepository _matriculaAlunoTurmaRepository;
        private readonly IVMatriculaAlunoTurmaRepository _vMatriculaAlunoTurmaRepository;
        private readonly IFrequenciaRepository _frequenciaRepository;
        private readonly IVFrequenciaAlunoTurmaRepository _vFrequenciaAlunoTurmaRepository;
        private readonly IMapper _mapper;

        public TurmaService(
            INotificador notificador,
            IObjectExtensionGenerics<TurmaEntity> objectExtensionGenerics,
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMatriculaAlunoTurmaRepository matriculaAlunoTurmaRepository,
            IVMatriculaAlunoTurmaRepository vMatriculaAlunoTurmaRepository,
            IFrequenciaRepository frequenciaRepository,
            IVFrequenciaAlunoTurmaRepository vFrequenciaAlunoTurmaRepository,
            IMapper mapper) : base(notificador, turmaRepository)
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

        public async Task<IEnumerable<TurmaOutput>> ListarTurmasAtivas()
        {
            var parametros = new Dictionary<string, object?>();
            parametros.Add("Ativo", true);

            var input = new InputCondicaoParametros()
            {
                Condicao = "Ativo = @Ativo",
                Parametros = parametros
            };

            var retorno = await _turmaRepository.RetornaListaByCondicaoAsync(input);
            _ = retorno
                .OrderByDescending(turma => turma.IdadeInicialAluno)
                .OrderBy(turma => turma.SemestreLetivo)
                .OrderBy(turma => turma.AnoLetivo);

            var turmaOutput = _mapper.Map<List<TurmaOutput>>(retorno);

            return turmaOutput;
        }
    }
}

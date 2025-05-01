using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.IO.Matricula;

namespace TRILHAR.Business.Services
{
    public class VMatriculaService : ServiceGenericsBase<VMatriculaEntity>, IVMatriculaService
    {
        private readonly IObjectExtensionGenerics<VMatriculaEntity> _objectExtensionGenerics;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IVMatriculaRepository _vMatriculaRepository;
        private readonly IFrequenciaRepository _frequenciaRepository;
        private readonly IVFrequenciaRepository _vFrequenciaRepository;

        public VMatriculaService(
            INotificador notificador,
            IObjectExtensionGenerics<VMatriculaEntity> objectExtensionGenerics,
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMatriculaRepository matriculaRepository,
            IVMatriculaRepository vMatriculaRepository,
            IFrequenciaRepository frequenciaRepository,
            IVFrequenciaRepository vFrequenciaRepository,
            IMapper mapper) : base(notificador, mapper, vMatriculaRepository)
        {
            _objectExtensionGenerics = objectExtensionGenerics;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
            _matriculaRepository = matriculaRepository;
            _vMatriculaRepository = vMatriculaRepository;
            _frequenciaRepository = frequenciaRepository;
            _vFrequenciaRepository = vFrequenciaRepository;
        }

        public async Task<IEnumerable<VMatriculaOutput>?> ListarPorCodigoAlunoCodigoTurma(int codigoAluno, int codigoTurma)
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

            var listaMatriculas = await _vMatriculaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);

            var resultado = _mapper.Map<IEnumerable<VMatriculaOutput>>(listaMatriculas);

            return resultado.Any() ? resultado : null;
        }

        public async Task<IEnumerable<VMatriculaOutput>?> ListarPorCodigoAluno(int codigoAluno)
        {
            var inputCondicaoParametros = new InputCondicaoParametros
            {
                Condicao = "CodigoAluno = @CodigoAluno",
                Parametros = new Dictionary<string, object?>
                {
                    { "@CodigoAluno", codigoAluno }
                }
            };

            var listaMatriculas = await _vMatriculaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);

            var resultado = _mapper.Map<IEnumerable<VMatriculaOutput>>(listaMatriculas);

            return resultado.Any() ? resultado : null;
        }

        public async Task<IEnumerable<VMatriculaOutput>?> ListarPorCodigoTurma(int codigoTurma)
        {
            var inputCondicaoParametros = new InputCondicaoParametros
            {
                Condicao = "CodigoTurma = @CodigoTurma",
                Parametros = new Dictionary<string, object?>
                {
                    { "@CodigoTurma", codigoTurma }
                }
            };

            var listaMatriculas = await _vMatriculaRepository.RetornaListaByCondicaoAsync(inputCondicaoParametros);

            var resultado = _mapper.Map<IEnumerable<VMatriculaOutput>>(listaMatriculas);

            return resultado.Any() ? resultado : null;
        }
    }
}

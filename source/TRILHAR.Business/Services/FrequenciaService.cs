using AutoMapper;
using System.Data;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO.Frequencia;

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
        private readonly IVFrequenciaService _vFrequenciaService;
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
            IVFrequenciaService vFrequenciaService,
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
            _vFrequenciaService = vFrequenciaService;
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
            model.DataAtualizacao = DateTime.Now;
            model.DataFrequencia = new DateTime(
                input.DataFrequencia.Value.Year, 
                input.DataFrequencia.Value.Month, 
                input.DataFrequencia.Value.Day, 
                model.DataAtualizacao.Value.Hour, 
                model.DataAtualizacao.Value.Minute, 
                model.DataAtualizacao.Value.Second);

            var retorno = await _vFrequenciaService.GetByAlunoAndTurmaAndDateAsync(model.CodigoAluno, model.CodigoTurma, model.DataFrequencia??DateTime.Now);
            
            if(retorno != null && retorno.Any(x => x.Presenca))
            {
                var primeiro = retorno.Where(x => x.Presenca).First();
                if (primeiro.Presenca) return primeiro.Codigo;
                else
                {
                    model.Codigo = primeiro.Codigo;
                    model.DataAtualizacao = DateTime.Now;
                    model.DataFrequencia = input.DataFrequencia ?? DateTime.Now;
                    var ret = await _frequenciaRepository.UpdateAsync(model);
                    if (ret) return model.Codigo;
                    else return 0;
                }
            }

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

        }
}

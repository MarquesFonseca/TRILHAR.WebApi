using AutoMapper;
using System.Globalization;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO.Aluno;

namespace TRILHAR.Business.Services
{
    public class AlunoService : ServiceGenericsBase<AlunoEntity>, IAlunoService
    {
        private readonly IObjectExtensionGenerics<AlunoEntity> _objectExtensionGenerics;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IMatriculaAlunoTurmaRepository _matriculaAlunoTurmaRepository;
        private readonly IVMatriculaAlunoTurmaRepository _vMatriculaAlunoTurmaRepository;
        private readonly IFrequenciaRepository _frequenciaRepository;
        private readonly IVFrequenciaAlunoTurmaRepository _vFrequenciaAlunoTurmaRepository;
        private readonly IMapper _mapper;

        public AlunoService(
            INotificador notificador,
            IObjectExtensionGenerics<AlunoEntity> objectExtensionGenerics,
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository,
            IMatriculaAlunoTurmaRepository matriculaAlunoTurmaRepository,
            IVMatriculaAlunoTurmaRepository vMatriculaAlunoTurmaRepository,
            IFrequenciaRepository frequenciaRepository,
            IVFrequenciaAlunoTurmaRepository vFrequenciaAlunoTurmaRepository,
            IMapper mapper) : base(notificador, alunoRepository)
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
        
        public async Task<int> InsertAsync(AlunoInput entity)
        {
            var model = _mapper.Map<AlunoInput, AlunoEntity>(entity);

            model = _objectExtensionGenerics.TrataCamposNulls(model);
            
            model = TratamentoCamposComuns(model);
            
            model.DataCadastro = model.DataAtualizacao = DateTime.Now;
            
            var maxCodigoCadastro = await _alunoRepository.RetornaMaxCodigoCadastroAsync();
            
            model.CodigoCadastro = Convert.ToString(maxCodigoCadastro + 1);

            return await _alunoRepository.InsertAsync(model);
        }

        public async Task<int> InsertAsync(IEnumerable<AlunoInput> list)
        {
            var models = new List<AlunoEntity>();
            foreach (var item in list)
            {
                var model = _mapper.Map<AlunoInput, AlunoEntity>(item);
                
                model = _objectExtensionGenerics.TrataCamposNulls(model);
                
                model = TratamentoCamposComuns(model);
                
                model.DataCadastro = model.DataAtualizacao = DateTime.Now;
                
                var maxCodigoCadastro = await _alunoRepository.RetornaMaxCodigoCadastroAsync();
                
                model.CodigoCadastro = Convert.ToString(maxCodigoCadastro + 1);
                
                models.Add(model);
            }

            return await _alunoRepository.InsertAsync(models);
        }

        public async Task<bool> UpdateAsync(AlunoInput entity)
        {
            var model = _mapper.Map<AlunoInput, AlunoEntity>(entity);

            model = _objectExtensionGenerics.TrataCamposNulls(model);

            model = TratamentoCamposComuns(model);

            model.DataAtualizacao = DateTime.Now;

            return await _alunoRepository.UpdateAsync(model);
        }

        public async Task<bool> UpdateAsync(IEnumerable<AlunoInput> list)
        {
            var models = new List<AlunoEntity>();
            foreach (var item in list)
            {
                var model = _mapper.Map<AlunoInput, AlunoEntity>(item);
                
                model = _objectExtensionGenerics.TrataCamposNulls(model);

                model = TratamentoCamposComuns(model);

                model.DataAtualizacao = DateTime.Now;

                models.Add(model);
            }

            return await _alunoRepository.UpdateAsync(models);
        }

        private AlunoEntity TratamentoCamposComuns(AlunoEntity model)
        {
            if (model.NomeCrianca != null)
            {
                model.NomeCrianca = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.NomeCrianca.ToLowerInvariant());
            }
            if (model.DataNascimento.HasValue)
            {
                model.DataNascimento = model.DataNascimento.GetValueOrDefault().Date;
            }
            if (model.DataBatizado.HasValue)
            {
                model.DataBatizado = model.DataBatizado.GetValueOrDefault().Date;
            }
            if (model.NomeMae != null)
            {
                model.NomeMae = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.NomeMae.ToLowerInvariant());
            }
            if (model.NomePai != null)
            {
                model.NomePai = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.NomePai.ToLowerInvariant());
            }
            if (model.OutroResponsavel != null)
            {
                model.OutroResponsavel = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.OutroResponsavel.ToLowerInvariant());
            }
            if (model.EnderecoEmail != null)
            {
                model.EnderecoEmail = model.EnderecoEmail.ToLowerInvariant();
            }

            return model;
        }
    }
}

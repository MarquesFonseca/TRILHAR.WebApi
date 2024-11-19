using AutoMapper;
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
        private readonly IAlunoRepository _repository;
        private readonly IMapper _mapper;

        public AlunoService(
            INotificador notificador,
            IAlunoRepository repository,
            IObjectExtensionGenerics<AlunoEntity> objectExtension,
            IMapper mapper
            ) : base(notificador, repository)
        {
            _objectExtensionGenerics = objectExtension;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> InsertAsync(AlunoInput entity)
        {
            var model = _mapper.Map<AlunoInput, AlunoEntity>(entity);

            model = _objectExtensionGenerics.TrataCamposNulls(model);

            return await _repository.InsertAsync(model);
        }

        public async Task<int> InsertAsync(IEnumerable<AlunoInput> list)
        {
            var models = new List<AlunoEntity>();
            foreach (var item in list)
            {
                var model = _mapper.Map<AlunoInput, AlunoEntity>(item);
                model = _objectExtensionGenerics.TrataCamposNulls(model);
                models.Add(model);
            }

            return await _repository.InsertAsync(models);
        }

        public async Task<bool> UpdateAsync(AlunoInput entity)
        {
            var model = _mapper.Map<AlunoInput, AlunoEntity>(entity);

            model = _objectExtensionGenerics.TrataCamposNulls(model);

            return await _repository.UpdateAsync(model);
        }

        public async Task<bool> UpdateAsync(IEnumerable<AlunoInput> list)
        {
            var models = new List<AlunoEntity>();
            foreach (var item in list)
            {
                var model = _mapper.Map<AlunoInput, AlunoEntity>(item);
                model = _objectExtensionGenerics.TrataCamposNulls(model);
                models.Add(model);
            }

            return await _repository.UpdateAsync(models);
        }
    }
}

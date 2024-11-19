using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Notificador;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.Interfaces.Services;
using TRILHAR.Business.IO;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.Services
{
    public abstract class ServiceGenericsBase<TEntity> : BaseService, IServiceGenericsBase<TEntity> where TEntity : EntityBase
    {
        private readonly IRepositoryGenericsBase<TEntity> _repository;

        public ServiceGenericsBase(INotificador notificador, IRepositoryGenericsBase<TEntity> repository)
        : base(notificador)
        {
            _repository = repository;
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync() => _repository.GetAllAsync();

        public virtual Task<TEntity> GetByCodigoAsync(int codigo) => _repository.GetByCodigoAsync(codigo);

        public virtual Task<PagedResult<TEntity>> GetByPaginacaoAsync(InputPaginado input) => _repository.GetByPaginacaoAsync(input);

        public virtual Task<int> InsertAsync(TEntity entity) => _repository.InsertAsync(entity);

        public virtual Task<int> InsertAsync(IEnumerable<TEntity> list) => _repository.InsertAsync(list);

        public virtual Task<bool> UpdateAsync(TEntity entity) => _repository.UpdateAsync(entity);

        public virtual Task<bool> UpdateAsync(IEnumerable<TEntity> list) => _repository.UpdateAsync(list);

        public virtual Task<bool> DeleteAsync(TEntity entity) => _repository.DeleteAsync(entity);

        public virtual Task<bool> DeleteAsync(IEnumerable<TEntity> list) => _repository.DeleteAsync(list);

        public virtual Task<int> DeleteByCodigoAsync(int codigo) => _repository.DeleteByCodigoAsync(codigo);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
        }
    }
}

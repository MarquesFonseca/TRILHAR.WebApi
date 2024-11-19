using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.Interfaces.Services
{
    public interface IServiceGenericsBase<TEntity> : IDisposable where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByCodigoAsync(int codigo);
        Task<PagedResult<TEntity>> GetByPaginacaoAsync(InputPaginado input);
        Task<int> InsertAsync(TEntity entity);
        Task<int> InsertAsync(IEnumerable<TEntity> list);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> UpdateAsync(IEnumerable<TEntity> list);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> DeleteAsync(IEnumerable<TEntity> list);
        Task<int> DeleteByCodigoAsync(int codigo);
    }
}

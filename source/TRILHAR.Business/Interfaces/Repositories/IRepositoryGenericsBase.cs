using System.Data;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.Interfaces.Repositories
{
    public interface IRepositoryGenericsBase<TEntity> : IDisposable where TEntity : EntityBase
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
        Task<int> ExecuteAsync(InputConsultaPersonalizada input, CommandType commandType = CommandType.Text);
        Task<int> RetornaMaxCodigoAsync();
        Task<TEntity?> RetornaSingleBySqlConsultaCondicao(InputConsultaPersonalizada input);
        Task<IEnumerable<TEntity>> RetornaListaBySqlConsultaCondicao(InputConsultaPersonalizada input);
        Task<TEntity?> RetornaByCondicaoAsync(InputCondicaoParametros input);
        Task<IEnumerable<TEntity>> RetornaListaByCondicaoAsync(InputCondicaoParametros input);
    }
}

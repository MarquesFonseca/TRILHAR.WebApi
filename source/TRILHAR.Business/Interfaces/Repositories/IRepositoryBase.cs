using System.Linq.Expressions;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Paginacao;

namespace TRILHAR.Business.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : EntityBase
    {
        Task<TEntity> Get(Expression<Func<TEntity, bool>> expressionSearch, List<Expression<Func<TEntity, object>>> expressionIncludes = null);
        Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>> expressionSearch, List<Expression<Func<TEntity, object>>> expressionIncludes = null);
        Task<Paginacao<TEntity>> GetPaged(Expression<Func<TEntity, bool>> expressionSearch, List<Expression<Func<TEntity, object>>> expressionIncludes, int page, int pageSize);
        Task<Paginacao<TEntity>> GetPagedByListConditions(List<Expression<Func<TEntity, bool>>> expressionSearch, List<Expression<Func<TEntity, object>>> expressionIncludes, int page = 1, int pageSize = 10);
        Task<TEntity> Add(TEntity obj);
        Task<List<TEntity>> AddRange(List<TEntity> listObj);
        Task<TEntity> Update(TEntity obj);
        Task<List<TEntity>> UpdateRange(List<TEntity> listObj);
        Task Remove(object id);
        Task RemoveEntity(TEntity obj);
        Task RemoveRange(IEnumerable<TEntity> obj);
        Task<bool> Exists(Expression<Func<TEntity, bool>> expression);
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TRILHAR.Business.Entities;

namespace TRILHAR.Business.Interfaces.Repositories
{
    public interface IRepositoryGenericsBase<TEntity> : IDisposable where TEntity : EntityBase
    {
        Task<TEntity> GetAsync(int codigo);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> InsertAsync(TEntity entity);
        Task<int> InsertAsync(IEnumerable<TEntity> list);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> UpdateAsync(IEnumerable<TEntity> list);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> DeleteAsync(IEnumerable<TEntity> list);
        Task<int> ExecuteAsync(string sql, string condicao = null, object parametros = null, CommandType commandType = CommandType.Text);
        Task<TEntity> RetornaSingleBySqlConsultaCondicao(string sqlConsulta, string condicao = null, object parametros = null);
        Task<IEnumerable<TEntity>> RetornaListaBySqlConsultaCondicao(string sqlConsulta, string condicao = null, object parametros = null);
    }
}

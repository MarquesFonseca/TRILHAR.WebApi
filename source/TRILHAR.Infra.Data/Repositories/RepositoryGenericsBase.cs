using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;
using Dapper;
using Dapper.Contrib.Extensions;

namespace TRILHAR.Infra.Data.Repositories
{
    public abstract class RepositoryGenericsBase<SqlConnection, TEntity> : IRepositoryGenericsBase<TEntity> where TEntity : EntityBase where SqlConnection : IDbConnection
    {
        private readonly SqlConnection _sqlConnection;

        public RepositoryGenericsBase(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public virtual async Task<TEntity> GetAsync(int codigo)
        {
            return await _sqlConnection.GetAsync<TEntity>(codigo);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _sqlConnection.GetAllAsync<TEntity>();
        }

        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            return await _sqlConnection.InsertAsync(entity);
        }

        public virtual async Task<int> InsertAsync(IEnumerable<TEntity> list)
        {
            return await _sqlConnection.InsertAsync(list);
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            return await _sqlConnection.UpdateAsync(entity);
        }
        public virtual async Task<bool> UpdateAsync(IEnumerable<TEntity> list)
        {
            return await _sqlConnection.UpdateAsync(list);
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            return await _sqlConnection.DeleteAsync(entity);
        }

        public virtual async Task<bool> DeleteAsync(IEnumerable<TEntity> list)
        {
            return await _sqlConnection.DeleteAsync(list);
        }

        public virtual Task<int> ExecuteAsync(string sql, string condicao = null, object parametros = null, CommandType commandType = CommandType.Text)
        {
            string stringSql = $"{sql} ";
            if (condicao != null) stringSql += $"WHERE { condicao} ";

            if (commandType == CommandType.Text)
            {
                var retornoLinhasAfetadas = _sqlConnection.ExecuteAsync(sql: stringSql, param: parametros, commandType: commandType);
                return retornoLinhasAfetadas;
            }
            if (commandType == CommandType.StoredProcedure)
            {
                var retorno = _sqlConnection.ExecuteAsync(sql: stringSql, param: parametros);
                return retorno;
            }
            return Task.FromResult(0);
        }

        public async Task<TEntity> RetornaSingleBySqlConsultaCondicao(string sqlConsulta, string condicao = null, object parametros = null)
        {
            string stringSql = $"{sqlConsulta} ";
            if (condicao != null) stringSql += $"WHERE { condicao} ";

            var retornaSingle = _sqlConnection.QuerySingleOrDefaultAsync<TEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
            return await retornaSingle;
        }

        public async Task<IEnumerable<TEntity>> RetornaListaBySqlConsultaCondicao(string sqlConsulta, string condicao = null, object parametros = null)
        {
            string stringSql = $"{sqlConsulta} ";
            if (condicao != null) stringSql += $"WHERE { condicao} ";

            var retornaLista = _sqlConnection.QueryAsync<TEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
            return await retornaLista;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sqlConnection.Dispose();
            }
        }
    }
}

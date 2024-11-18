using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Trilhar.Forms.Repository.Extensions;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.IO.Paginacao;
using TRILHAR.Business.Pagination;
using TRILHAR.Infra.Data.Extensions;

namespace TRILHAR.Infra.Data.Repositories
{
    public abstract class RepositoryGenericsBase<SqlConnection, TEntity> : IRepositoryGenericsBase<TEntity> where TEntity : EntityBase where SqlConnection : IDbConnection
    {
        private readonly SqlConnection _sqlConnection;

        public RepositoryGenericsBase(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _sqlConnection.GetAllAsync<TEntity>();
        }

        public virtual async Task<TEntity> GetByCodigoAsync(int codigo)
        {
            return await _sqlConnection.GetAsync<TEntity>(codigo);
        }

        //public virtual async Task<TEntity?> RetornaByCodigoAsync(int codigo)
        //{
        //    var parametros = new { Codigo = codigo };

        //    string stringSql = $"SELECT {ObterCampos()} FROM {ObterTabela()} WHERE Codigo = @Codigo";

        //    var retornaAluno = await _sqlConnection.QuerySingleOrDefaultAsync<TEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);

        //    return retornaAluno;
        //}

        public virtual async Task<PagedResult<TEntity>> GetByPaginacaoAsync(InputPaginado input)
        {
            var retorno = new PagedResult<TEntity>();
            var parametros = new DynamicParameters();
            int qtdRegistros;
            string stringSql = $"SELECT {ObterCampos()} FROM {ObterTabela()} ORDER BY Codigo";
            string stringQtdRegistrosSql = $"SELECT COUNT(*) AS Quantidade FROM {ObterTabela()}";

            if (input.Parametros != null && input.Parametros.Any())
            {
                parametros = RepositoryExtension.ConverterParaParametrosDapper(input.Parametros);
                stringSql = $"SELECT {ObterCampos()} FROM {ObterTabela()} WHERE {input.Condicao} ORDER BY Codigo";
                stringQtdRegistrosSql = $"SELECT COUNT(*) AS Quantidade FROM {ObterTabela()} WHERE {input.Condicao}";
            }

            qtdRegistros = await _sqlConnection.QuerySingleAsync<int>(stringQtdRegistrosSql, param: parametros, commandType: CommandType.Text);

            RepositoryExtension.TratamentoPaginacao(ref stringSql, ref parametros, input.IsPaginacao, input.Page, input.PageSize);

            var resultados = await _sqlConnection.QueryAsync<TEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);

            retorno = resultados.ToList().GetPaged(qtdRegistros, input.Page, input.PageSize);

            return retorno;
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
        
        public virtual Task<int> ExecuteAsync(string? sql, string? condicao = null, object? parametros = null, CommandType commandType = CommandType.Text)
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
        
        public virtual async Task<TEntity?> RetornaSingleBySqlConsultaCondicao(string? sqlConsulta, string? condicao = null, object? parametros = null)
        {
            string stringSql = $"{sqlConsulta} ";
            if (condicao != null) stringSql += $"WHERE { condicao} ";

            var retornaSingle = _sqlConnection.QuerySingleOrDefaultAsync<TEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
            return await retornaSingle;
        }
        
        public virtual async Task<IEnumerable<TEntity>> RetornaListaBySqlConsultaCondicao(string? sqlConsulta, string? condicao = null, object? parametros = null)
        {
            string stringSql = $"{sqlConsulta} ";
            if (condicao != null) stringSql += $"WHERE { condicao} ";

            var retornaLista = _sqlConnection.QueryAsync<TEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
            return await retornaLista;
        }

        public virtual async Task<TEntity?> RetornaByCondicaoAsync(string? condicao, object? parametros)
        {
            string stringSql = $"SELECT {ObterCampos()} FROM {ObterTabela()} " +
                $"WHERE {condicao}";

            var retornaAluno = await _sqlConnection.QuerySingleOrDefaultAsync<TEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
            
            return retornaAluno;
        }

        public virtual async Task<IEnumerable<TEntity>> RetornaListaByCondicaoAsync(string? condicao, object? parametros)
        {
            string stringSql = $"SELECT {ObterCampos()} FROM {ObterTabela()} " +
               $"WHERE {condicao}";

            var retornaAluno = await _sqlConnection.QueryAsync<TEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
            
            return retornaAluno;
        }               

        protected abstract string ObterCampos();
        
        protected abstract string ObterTabela();
        
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

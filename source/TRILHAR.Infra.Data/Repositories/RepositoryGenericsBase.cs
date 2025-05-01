using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Trilhar.Forms.Repository.Extensions;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Business.IO;
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

        public virtual async Task<TEntity> GetByCodigoAsync(int? codigo)
        {
            return await _sqlConnection.GetAsync<TEntity>(codigo);
        }

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

            retorno = resultados.ToList().GetPaged(qtdRegistros, input.Page, input.PageSize, input.IsPaginacao);

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

        public virtual async Task<int> DeleteByCodigoAsync(int codigo)
        {
            var parametros = new { Codigo = codigo };

            string stringSql = $"DELETE FROM {ObterTabela()} " +
                $"WHERE Codigo = @Codigo";

            var retorno = await _sqlConnection.ExecuteAsync(sql: stringSql, param: parametros, commandType: CommandType.Text);
            
            return retorno;
        }

        public virtual Task<int> ExecuteAsync(InputConsultaPersonalizada input)
        {
            string stringSql = $"{input.ConsultaPersonalizada} ";
            if (input.Condicao != null) stringSql += $"WHERE { input.Condicao} ";

            var parametros = RepositoryExtension.ConverterParaParametrosDapper(input.Parametros);

            if (input.CommandType == CommandType.Text)
            {
                var retornoLinhasAfetadas = _sqlConnection.ExecuteAsync(sql: stringSql, param: parametros, commandType: input.CommandType);
                return retornoLinhasAfetadas;
            }
            if (input.CommandType == CommandType.StoredProcedure)
            {
                var retorno = _sqlConnection.ExecuteAsync(sql: stringSql, param: parametros);
                return retorno;
            }
            return Task.FromResult(0);
        }

        public virtual async Task<int> RetornaMaxCodigoAsync()
        {
            string stringSql = $"SELECT MAX(Codigo) FROM {ObterTabela()} ";
            var retornaMax = await _sqlConnection.QueryFirstOrDefaultAsync<int>(sql: stringSql, commandType: CommandType.Text);
            return retornaMax;
        }

        public virtual async Task<TEntity?> RetornaSingleBySqlConsultaCondicao(InputConsultaPersonalizada input)
        {
            string stringSql = $"{input.ConsultaPersonalizada} ";
            if (input.Condicao != null) stringSql += $"WHERE {input.Condicao} ";

            var parametros = RepositoryExtension.ConverterParaParametrosDapper(input.Parametros);
            
            var retornaSingle = await _sqlConnection.QuerySingleOrDefaultAsync<TEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
            
            return retornaSingle;
        }
        
        public virtual async Task<IEnumerable<TEntity>> RetornaListaBySqlConsultaCondicao(InputConsultaPersonalizada input)
        {
            string stringSql = $"{input.ConsultaPersonalizada} ";
            if (input.Condicao != null) stringSql += $"WHERE {input.Condicao} ";

            var parametros = RepositoryExtension.ConverterParaParametrosDapper(input.Parametros);

            var retornaLista = _sqlConnection.QueryAsync<TEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
            return await retornaLista;
        }

        public virtual async Task<TEntity?> RetornaByCondicaoAsync(InputCondicaoParametros input)
        {
            string stringSql = $"SELECT {ObterCampos()} FROM {ObterTabela()} " +
                $"WHERE {input.Condicao}";

            var parametros = RepositoryExtension.ConverterParaParametrosDapper(input.Parametros);

            var retornaSingle = await _sqlConnection.QuerySingleOrDefaultAsync<TEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
            
            return retornaSingle;
        }

        public virtual async Task<IEnumerable<TEntity>> RetornaListaByCondicaoAsync(InputCondicaoParametros input)
        {
            string stringSql = $"SELECT {ObterCampos()} FROM {ObterTabela()} " +
               $"WHERE {input.Condicao}";

            var parametros = RepositoryExtension.ConverterParaParametrosDapper(input.Parametros);

            var retornaLista = await _sqlConnection.QueryAsync<TEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
            
            return retornaLista;
        }               

        public virtual PagedResult<TEntity> RetornaPagedResultAsync(IEnumerable<TEntity> resultados, int page, int pageSize, bool isPaginacao)
        {
            var qtdRegistros = resultados.Count();
            page = page == 0 ? 1 : page;
            pageSize = pageSize == 0 ? 10 : pageSize;

            var retorno = new PagedResult<TEntity>();
            retorno = resultados.ToList().GetPaged(qtdRegistros, page, pageSize, isPaginacao);

            return retorno;
        }

        public virtual async Task<IEnumerable<TEntity>> QueryDynamicSql(InputConsultaPersonalizada input)
        {
            string stringSql = $"{input.ConsultaPersonalizada} ";
            if (input.Condicao != null) stringSql += $"WHERE {input.Condicao} ";

            var parametros = RepositoryExtension.ConverterParaParametrosDapper(input.Parametros);

            //var retorno = new List<T>();

            if (input.CommandType == CommandType.Text)
            {
                var retorno = await _sqlConnection.QueryAsync<TEntity>(sql: stringSql, param: parametros, commandType: input.CommandType);
                return retorno;
            }
            if (input.CommandType == CommandType.StoredProcedure)
            {
                var retorno = await _sqlConnection.QueryAsync<TEntity>(sql: stringSql, param: parametros);
                return retorno;
            }
            return new List<TEntity>();
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

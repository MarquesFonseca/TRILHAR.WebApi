using Dapper;

namespace Trilhar.Forms.Repository.Extensions
{
    public static class RepositoryExtension
    {
        /// <summary>
        /// Tratamento da pagaginação quando todos for false
        /// </summary>
        /// <param name="parametros">Informe os parametros condicionais</param>
        /// <param name="isPaginacao">Informe se exibir todos. Se marcar 'False', então atribuirá a paginação</param>
        /// <param name="page">Informe a Página atual</param>
        /// <param name="pageSize">Informe a Quantidade de registros por página.</param>
        /// <param name="stringSql">Informe a stringSql</param>
        public static void TratamentoPaginacao(ref string stringSql, ref object parametros, bool isPaginacao = false, int page = 1, int pageSize = 10)
        {
            page = page == 0 ? 1 : page;
            int skip = (page - 1) * pageSize;
            if (isPaginacao)
            {
                stringSql += $"OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";
                var parametrosComPaginacao = new DynamicParameters(parametros);
                parametrosComPaginacao.Add("Skip", skip);
                parametrosComPaginacao.Add("Take", pageSize);

                parametros = parametrosComPaginacao;
            }
        }
    }
}

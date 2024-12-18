using System.Data.SqlClient;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;

namespace TRILHAR.Infra.Data.Repositories
{
    public class PaginaRepository : RepositoryGenericsBase<SqlConnection, PaginaEntity>, IPaginaRepository
    {
        private readonly SqlConnection _conn;
        public PaginaRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            _conn = sqlConnection;
        }

        protected override string ObterCampos() => "Codigo, Descricao, Rota, CodigoUsuarioCadastro, DataAtualizacao, DataCadastro";

        protected override string ObterTabela() => "Pagina";
    }
}

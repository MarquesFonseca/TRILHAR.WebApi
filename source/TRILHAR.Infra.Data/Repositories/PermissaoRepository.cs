using System.Data.SqlClient;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;

namespace TRILHAR.Infra.Data.Repositories
{
    public class PermissaoRepository : RepositoryGenericsBase<SqlConnection, PermissaoEntity>, IPermissaoRepository
    {
        private readonly SqlConnection _conn;
        public PermissaoRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            _conn = sqlConnection;
        }

        protected override string ObterCampos() => "Codigo, CodigoUsuario, CodigoPagina, Acesso, CodigoUsuarioCadastro, DataAtualizacao, DataCadastro";

        protected override string ObterTabela() => "Permissao";
    }
}

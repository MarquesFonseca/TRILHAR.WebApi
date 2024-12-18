using System.Data.SqlClient;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;

namespace TRILHAR.Infra.Data.Repositories
{
    public class UsuarioRepository : RepositoryGenericsBase<SqlConnection, UsuarioEntity>, IUsuarioRepository
    {
        private readonly SqlConnection _conn;
        public UsuarioRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            _conn = sqlConnection;
        }

        protected override string ObterCampos() => "Codigo, Cpf, Nome, Telefone, TelefoneAlternativo, Email, Senha, DiasExpiracao, NivelConsulta, NivelAtualizacao, DataUltimoAcesso, NuTentativa, Ativo, CodigoUsuarioCadastro, DataAtualizacao, DataCadastro, CodigoDepartamento, NomeFuncao";

        protected override string ObterTabela() => "Usuario";
    }
}

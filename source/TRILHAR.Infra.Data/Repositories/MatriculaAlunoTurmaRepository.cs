using System.Data.SqlClient;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;

namespace TRILHAR.Infra.Data.Repositories
{
    public class MatriculaAlunoTurmaRepository : RepositoryGenericsBase<SqlConnection, MatriculaAlunoTurmaEntity>, IMatriculaAlunoTurmaRepository
    {
        private readonly SqlConnection db;
        public MatriculaAlunoTurmaRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            db = sqlConnection;
        }

        protected override string ObterCampos() => "Codigo, CodigoAluno, CodigoTurma, Ativo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro";

        protected override string ObterTabela() => "MatriculaAlunoTurma";

    }
}

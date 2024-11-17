using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Infra.Data.EF;

namespace TRILHAR.Infra.Data.Repositories
{
    public class MatriculaAlunoTurmaRepository : RepositoryGenericsBase<SqlConnection, MatriculaAlunoTurmaEntity>, IMatriculaAlunoTurmaRepository
    {
        private readonly SqlConnection _conn;
        public MatriculaAlunoTurmaRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            _conn = sqlConnection;
        }

        protected override string ObterCampos() => "";

        protected override string ObterTabela() => "MatriculaAlunoTurma";

    }
}

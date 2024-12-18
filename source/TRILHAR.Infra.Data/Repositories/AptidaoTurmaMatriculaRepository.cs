using System.Data.SqlClient;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;

namespace TRILHAR.Infra.Data.Repositories
{
    public class AptidaoTurmaMatriculaRepository : RepositoryGenericsBase<SqlConnection, AptidaoTurmaMatriculaEntity>, IAptidaoTurmaMatriculaRepository
    {
        private readonly SqlConnection db;
        public AptidaoTurmaMatriculaRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            db = sqlConnection;
        }

        protected override string ObterCampos() => "";

        protected override string ObterTabela() => "";


    }
}

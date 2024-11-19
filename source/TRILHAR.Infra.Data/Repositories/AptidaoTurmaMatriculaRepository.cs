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

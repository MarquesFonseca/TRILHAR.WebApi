using System.Data.SqlClient;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;

namespace TRILHAR.Infra.Data.Repositories
{
    public class FrequenciasTurmasAgrupadasRepository : RepositoryGenericsBase<SqlConnection, FrequenciasTurmasAgrupadasEntity>, IFrequenciasTurmasAgrupadasRepository
    {
        private readonly SqlConnection _conn;
        public FrequenciasTurmasAgrupadasRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            _conn = sqlConnection;
        }

        protected override string ObterCampos() => "DataFrequencia, CodigoTurma, TurmaDescricao, TurmaAnoLetivo, TurmaSemestreLetivo, TurmaIdadeInicialAluno, TurmaIdadeFinalAluno, TurmaAtivo, TurmaLimiteMaximo, Qtd";
        protected override string ObterTabela() => "SPFrequenciasTodasTurmasAgrupadasDia";
    }
}

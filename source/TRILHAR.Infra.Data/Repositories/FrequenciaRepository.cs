using System.Data.SqlClient;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;

namespace TRILHAR.Infra.Data.Repositories
{
    public class FrequenciaRepository : RepositoryGenericsBase<SqlConnection, FrequenciaEntity>, IFrequenciaRepository
    {
        private readonly SqlConnection db;
        public FrequenciaRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            db = sqlConnection;
        }

        protected override string ObterCampos() => "Codigo, DataFrequencia, CodigoAluno, CodigoTurma, Presenca, TurmaDescricao, TurmaIdadeInicialAluno, TurmaIdadeFinalAluno, TurmaAnoLetivo, TurmaSemestreLetivo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro";

        protected override string ObterTabela() => "Frequencia";


    }
}

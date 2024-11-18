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
    public class FrequenciaRepository : RepositoryGenericsBase<SqlConnection, FrequenciaEntity>, IFrequenciaRepository
    {
        private readonly SqlConnection _conn;
        public FrequenciaRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            _conn = sqlConnection;
        }

        protected override string ObterCampos() => "Codigo, DataFrequencia, CodigoAluno, CodigoTurma, Presenca, TurmaDescricao, TurmaIdadeInicialAluno, TurmaIdadeFinalAluno, TurmaAnoLetivo, TurmaSemestreLetivo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro";

        protected override string ObterTabela() => "Frequencia";


    }
}

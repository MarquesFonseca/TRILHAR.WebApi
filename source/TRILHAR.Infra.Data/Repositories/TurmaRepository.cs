using Dapper;
using System.Data;
using System.Data.SqlClient;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;

namespace TRILHAR.Infra.Data.Repositories
{
    public class TurmaRepository : RepositoryGenericsBase<SqlConnection, TurmaEntity>, ITurmaRepository
    {
        private readonly SqlConnection db;
        public TurmaRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            db = sqlConnection;
        }

        protected override string ObterCampos() => "Codigo, Descricao, IdadeInicialAluno, IdadeFinalAluno, AnoLetivo, SemestreLetivo, LimiteMaximo, Ativo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro";

        protected override string ObterTabela() => "Turma";

        public async Task<int> InsertOutputInsertedAsync(TurmaEntity entity)
        {
            const string stringSql = "INSERT INTO Turma OUTPUT INSERTED.Codigo " +
                "VALUES(@Descricao, @IdadeInicialAluno, @IdadeFinalAluno, @AnoLetivo, @SemestreLetivo, @LimiteMaximo, @Ativo, @CodigoUsuarioLogado, @DataAtualizacao, @DataCadastro)";

            var retorno = await db.ExecuteScalarAsync<int>(sql: stringSql, param: entity, commandType: CommandType.Text);
            return retorno;
        }
    }
}

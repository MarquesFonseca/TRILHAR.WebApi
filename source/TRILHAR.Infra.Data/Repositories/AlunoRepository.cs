using Dapper;
using System.Data;
using System.Data.SqlClient;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;

namespace TRILHAR.Infra.Data.Repositories
{
    public class AlunoRepository : RepositoryGenericsBase<SqlConnection, AlunoEntity>, IAlunoRepository
    {
        private readonly SqlConnection db;
        protected override string ObterCampos() => "Codigo, CodigoCadastro, NomeCrianca, DataNascimento, NomeMae, NomePai, OutroResponsavel, Telefone, EnderecoEmail, Alergia, DescricaoAlergia, RestricaoAlimentar, DescricaoRestricaoAlimentar, DeficienciaOuSituacaoAtipica, DescricaoDeficiencia, Batizado, DataBatizado, IgrejaBatizado, Ativo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro";
        protected override string ObterTabela() => "Aluno";

        public AlunoRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            db = sqlConnection;
        }

        public async Task<AlunoEntity?> RetornaByCodigoCadastroAsync(string codigoCadastro)
        {
            var parametros = new { CodigoCadastro = codigoCadastro };

            string stringSql = $"SELECT {ObterCampos()} FROM {ObterTabela()} " +
                $"WHERE CodigoCadastro = @CodigoCadastro";

            var retornaAluno = await db.QuerySingleOrDefaultAsync<AlunoEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);

            return retornaAluno;
        }

        public async Task<int> RetornaMaxCodigoCadastroAsync()
        {
            string stringSql = $"SELECT MAX(CodigoCadastro) FROM Aluno a ";
            var retornaMax = await db.QueryFirstOrDefaultAsync<int>(sql: stringSql, commandType: CommandType.Text);
            return retornaMax;
        }

        public async Task<int> InsertOutputInsertedAsync(AlunoEntity entity)
        {
            const string stringSql = "INSERT INTO Aluno OUTPUT INSERTED.Codigo " +
                "VALUES(@CodigoCadastro, @NomeCrianca, @DataNascimento, @NomeMae, @NomePai, @OutroResponsavel, @Telefone, @EnderecoEmail, @Alergia, @DescricaoAlergia, @RestricaoAlimentar, @DescricaoRestricaoAlimentar, @DeficienciaOuSituacaoAtipica, @DescricaoDeficiencia, @Batizado, @DataBatizado, @IgrejaBatizado, @Ativo, @CodigoUsuarioLogado, @DataAtualizacao, @DataCadastro)";

            var retorno = await db.ExecuteScalarAsync<int>(sql: stringSql, param: entity, commandType: CommandType.Text);
            return retorno;
        }
        
        public async Task<int> AtualizarRegistroAsync(AlunoEntity entity)
        {
            const string stringSql = "UPDATE Aluno SET " +
                                     "CodigoCadastro                = @CodigoCadastro,                  " +
                                     "NomeCrianca                   = @NomeCrianca,                     " +
                                     "DataNascimento                = @DataNascimento,                  " +
                                     "NomeMae                       = @NomeMae,                         " +
                                     "NomePai                       = @NomePai,                         " +
                                     "OutroResponsavel              = @OutroResponsavel,                " +
                                     "Telefone                      = @Telefone,                        " +
                                     "EnderecoEmail                 = @EnderecoEmail,                   " +
                                     "Alergia                       = @Alergia,                         " +
                                     "DescricaoAlergia              = @DescricaoAlergia,                " +
                                     "RestricaoAlimentar            = @RestricaoAlimentar,              " +
                                     "DescricaoRestricaoAlimentar   = @DescricaoRestricaoAlimentar,     " +
                                     "DeficienciaOuSituacaoAtipica  = @DeficienciaOuSituacaoAtipica,    " +
                                     "DescricaoDeficiencia          = @DescricaoDeficiencia,            " +
                                     "Batizado                      = @Batizado,                        " +
                                     "DataBatizado                  = @DataBatizado,                    " +
                                     "IgrejaBatizado                = @IgrejaBatizado,                  " +
                                     "Ativo                         = @Ativo,                           " +
                                     "CodigoUsuarioLogado           = @CodigoUsuarioLogado,             " +
                                     "DataAtualizacao               = @DataAtualizacao,                 " +
                                     "DataCadastro                  = @DataCadastro                     " +
                                     "WHERE Codigo                  = @Codigo                           ";

            var retorno = await db.ExecuteAsync(sql: stringSql, param: entity, commandType: CommandType.Text);
            return retorno;
        }
    }
}

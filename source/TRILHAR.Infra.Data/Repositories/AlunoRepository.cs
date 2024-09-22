using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Infra.Data.EF;

namespace TRILHAR.Infra.Data.Repositories
{
    public class AlunoRepository : RepositoryGenericsBase<SqlConnection, AlunoEntity>, IAlunoRepository
    {
        private readonly SqlConnection _conn;
        public AlunoRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            _conn = sqlConnection;
        }

        public async Task<IEnumerable<AlunoEntity>> RetornaAll()
        {
            using (IDbConnection db = new SqlConnection(_conn.ConnectionString))
            {
                const string stringSql = "SELECT " +
                    "Codigo, CodigoCadastro, NomeCrianca, DataNascimento, NomeMae, NomePai, OutroResponsavel, Telefone, EnderecoEmail, Alergia, DescricaoAlergia, RestricaoAlimentar, DescricaoRestricaoAlimentar, DeficienciaOuSituacaoAtipica, DescricaoDeficiencia, Batizado, DataBatizado, IgrejaBatizado, Ativo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro " +
                    "FROM Aluno";

                var retornaListaAluno = await db.QueryAsync<AlunoEntity>(sql: stringSql, commandType: CommandType.Text);
                return retornaListaAluno;
            }
        }

        public async Task<AlunoEntity> RetornaByCodigo(int codigo)
        {
            using (IDbConnection db = new SqlConnection(_conn.ConnectionString))
            {
                var parametros = new { Codigo = codigo };

                const string stringSql =
                    "SELECT " +
                    "Codigo, CodigoCadastro, NomeCrianca, DataNascimento, NomeMae, NomePai, OutroResponsavel, Telefone, EnderecoEmail, Alergia, DescricaoAlergia, RestricaoAlimentar, DescricaoRestricaoAlimentar, DeficienciaOuSituacaoAtipica, DescricaoDeficiencia, Batizado, DataBatizado, IgrejaBatizado, Ativo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro " +
                    "FROM Aluno " +
                    "WHERE Codigo = @Codigo";

                var retornaAluno = db.QuerySingleOrDefaultAsync<AlunoEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
                return await retornaAluno;
            }
        }

        public async Task<int> NovoRegistro(AlunoEntity entity)
        {
            using (IDbConnection db = new SqlConnection(_conn.ConnectionString))
            {
                const string stringSql = "INSERT INTO Aluno OUTPUT INSERTED.Codigo " +
                    //"(CodigoCadastro, NomeCrianca, DataNascimento, NomeMae, NomePai, OutroResponsavel, Telefone, EnderecoEmail, Alergia, DescricaoAlergia, RestricaoAlimentar, DescricaoRestricaoAlimentar, DeficienciaOuSituacaoAtipica, DescricaoDeficiencia, Batizado, DataBatizado, IgrejaBatizado, Ativo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro) " +
                    "VALUES(@CodigoCadastro, @NomeCrianca, @DataNascimento, @NomeMae, @NomePai, @OutroResponsavel, @Telefone, @EnderecoEmail, @Alergia, @DescricaoAlergia, @RestricaoAlimentar, @DescricaoRestricaoAlimentar, @DeficienciaOuSituacaoAtipica, @DescricaoDeficiencia, @Batizado, @DataBatizado, @IgrejaBatizado, @Ativo, @CodigoUsuarioLogado, @DataAtualizacao, @DataCadastro)";

                var retorno = db.ExecuteScalarAsync<int>(sql: stringSql, param: entity, commandType: CommandType.Text);
                return await retorno;
            }
        }

        public async Task<int> AtualizarRegistro(AlunoEntity entity)
        {
            using (IDbConnection db = new SqlConnection(_conn.ConnectionString))
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

                var retorno = db.ExecuteAsync(sql: stringSql, param: entity, commandType: CommandType.Text);
                return await retorno;
            }
        }

        public async Task<int> DeletarRegistro(int codigo)
        {
            using (IDbConnection db = new SqlConnection(_conn.ConnectionString))
            {
                var parametros = new { Codigo = codigo };

                const string stringSql = "DELETE FROM Aluno " +
                                         "WHERE Codigo = @Codigo";

                var retorno = db.ExecuteAsync(sql: stringSql, param: parametros, commandType: CommandType.Text);
                return await retorno;
            }
        }

        public async Task<AlunoEntity> RetornaByCodigoCadastro(string codigoCadastro)
        {
            using (IDbConnection db = new SqlConnection(_conn.ConnectionString))
            {
                var parametros = new { CodigoCadastro = codigoCadastro };

                const string stringSql =
                    "SELECT " +
                    "Codigo, CodigoCadastro, NomeCrianca, DataNascimento, NomeMae, NomePai, OutroResponsavel, Telefone, EnderecoEmail, Alergia, DescricaoAlergia, RestricaoAlimentar, DescricaoRestricaoAlimentar, DeficienciaOuSituacaoAtipica, DescricaoDeficiencia, Batizado, DataBatizado, IgrejaBatizado, Ativo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro " +
                    "FROM Aluno " +
                    "WHERE CodigoCadastro = @CodigoCadastro";

                var retornaAluno = await db.QuerySingleOrDefaultAsync<AlunoEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);


                return retornaAluno;
            }
        }

        public async Task<AlunoEntity> RetornaByCondicao(string condicao, object parametros)
        {
            using (IDbConnection db = new SqlConnection(_conn.ConnectionString))
            {
                string stringSql =
                    $"SELECT " +
                    $"Codigo, CodigoCadastro, NomeCrianca, DataNascimento, NomeMae, NomePai, OutroResponsavel, Telefone, EnderecoEmail, Alergia, DescricaoAlergia, RestricaoAlimentar, DescricaoRestricaoAlimentar, DeficienciaOuSituacaoAtipica, DescricaoDeficiencia, Batizado, DataBatizado, IgrejaBatizado, Ativo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro " +
                    $"FROM Aluno " +
                    $"WHERE {condicao}";

                var retornaAluno = db.QuerySingleOrDefaultAsync<AlunoEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
                return await retornaAluno;
            }
        }

        public async Task<IEnumerable<AlunoEntity>> RetornaListaByCondicao(string condicao, object parametros)
        {
            using (IDbConnection db = new SqlConnection(_conn.ConnectionString))
            {
                string stringSql =
                   $"SELECT " +
                   $"Codigo, CodigoCadastro, NomeCrianca, DataNascimento, NomeMae, NomePai, OutroResponsavel, Telefone, EnderecoEmail, Alergia, DescricaoAlergia, RestricaoAlimentar, DescricaoRestricaoAlimentar, DeficienciaOuSituacaoAtipica, DescricaoDeficiencia, Batizado, DataBatizado, IgrejaBatizado, Ativo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro " +
                   $"FROM Aluno " +
                   $"WHERE {condicao}";

                var retornaAluno = db.QueryAsync<AlunoEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
                return await retornaAluno;
            }
        }        
        
        public async Task<int> RetornaMaxCodigoCadastro()
        {
            using (IDbConnection db = new SqlConnection(_conn.ConnectionString))
            {
                string stringSql = $"SELECT MAX(CodigoCadastro) FROM Aluno a ";
                var retornaMax = db.QueryFirstOrDefaultAsync<int>(sql: stringSql, commandType: CommandType.Text);
                return await retornaMax;
            }
        }
    }
}

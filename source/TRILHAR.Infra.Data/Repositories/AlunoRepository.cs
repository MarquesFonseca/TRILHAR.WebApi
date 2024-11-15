using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Trilhar.Forms.Repository.Extensions;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;
using TRILHAR.Infra.Data.EF;

namespace TRILHAR.Infra.Data.Repositories
{
    public class AlunoRepository : RepositoryGenericsBase<SqlConnection, AlunoEntity>, IAlunoRepository
    {
        private readonly SqlConnection db;
        public AlunoRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            db = sqlConnection;
        }

        public async Task<IEnumerable<AlunoEntity>> RetornaAllAsync(bool isPaginacao = false, int page = 1, int pageSize = 10)
        {
            string stringSql = "SELECT " +
                    "Codigo, CodigoCadastro, NomeCrianca, DataNascimento, NomeMae, NomePai, OutroResponsavel, Telefone, EnderecoEmail, Alergia, DescricaoAlergia, RestricaoAlimentar, DescricaoRestricaoAlimentar, DeficienciaOuSituacaoAtipica, DescricaoDeficiencia, Batizado, DataBatizado, IgrejaBatizado, Ativo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro " +
                    "FROM Aluno " +
                    "ORDER BY Codigo ";

            if (isPaginacao)
            {
                var parametros = new object();
                RepositoryExtension.TratamentoPaginacao(ref stringSql, ref parametros, isPaginacao, page, pageSize);
                var retornaListaAluno1 = await db.QueryAsync<AlunoEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
                return retornaListaAluno1;
            }

            var retornaListaAluno = await db.QueryAsync<AlunoEntity>(sql: stringSql, commandType: CommandType.Text);
            return retornaListaAluno;
        }

        public async Task<AlunoEntity> RetornaByCodigoAsync(int codigo)
        {
            var parametros = new { Codigo = codigo };

            const string stringSql =
                "SELECT " +
                "Codigo, CodigoCadastro, NomeCrianca, DataNascimento, NomeMae, NomePai, OutroResponsavel, Telefone, EnderecoEmail, Alergia, DescricaoAlergia, RestricaoAlimentar, DescricaoRestricaoAlimentar, DeficienciaOuSituacaoAtipica, DescricaoDeficiencia, Batizado, DataBatizado, IgrejaBatizado, Ativo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro " +
                "FROM Aluno " +
                "WHERE Codigo = @Codigo";

            var retornaAluno = await db.QuerySingleOrDefaultAsync<AlunoEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
            return retornaAluno;
        }

        public async Task<int> NovoRegistroAsync(AlunoEntity entity)
        {
            const string stringSql = "INSERT INTO Aluno OUTPUT INSERTED.Codigo " +
                //"(CodigoCadastro, NomeCrianca, DataNascimento, NomeMae, NomePai, OutroResponsavel, Telefone, EnderecoEmail, Alergia, DescricaoAlergia, RestricaoAlimentar, DescricaoRestricaoAlimentar, DeficienciaOuSituacaoAtipica, DescricaoDeficiencia, Batizado, DataBatizado, IgrejaBatizado, Ativo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro) " +
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

        public async Task<int> DeletarRegistroAsync(int codigo)
        {
            var parametros = new { Codigo = codigo };

            const string stringSql = "DELETE FROM Aluno " +
                                     "WHERE Codigo = @Codigo";

            var retorno = await db.ExecuteAsync(sql: stringSql, param: parametros, commandType: CommandType.Text);
            return retorno;
        }

        public async Task<AlunoEntity> RetornaByCodigoCadastroAsync(string codigoCadastro)
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

        public async Task<AlunoEntity> RetornaByCondicaoAsync(string condicao, object parametros)
        {
            string stringSql =
                $"SELECT " +
                $"Codigo, CodigoCadastro, NomeCrianca, DataNascimento, NomeMae, NomePai, OutroResponsavel, Telefone, EnderecoEmail, Alergia, DescricaoAlergia, RestricaoAlimentar, DescricaoRestricaoAlimentar, DeficienciaOuSituacaoAtipica, DescricaoDeficiencia, Batizado, DataBatizado, IgrejaBatizado, Ativo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro " +
                $"FROM Aluno " +
                $"WHERE {condicao}";

            var retornaAluno = await db.QuerySingleOrDefaultAsync<AlunoEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
            return retornaAluno;
        }

        public async Task<IEnumerable<AlunoEntity>> RetornaListaByCondicaoAsync(string condicao, object parametros)
        {
            string stringSql =
               $"SELECT " +
               $"Codigo, CodigoCadastro, NomeCrianca, DataNascimento, NomeMae, NomePai, OutroResponsavel, Telefone, EnderecoEmail, Alergia, DescricaoAlergia, RestricaoAlimentar, DescricaoRestricaoAlimentar, DeficienciaOuSituacaoAtipica, DescricaoDeficiencia, Batizado, DataBatizado, IgrejaBatizado, Ativo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro " +
               $"FROM Aluno " +
               $"WHERE {condicao}";

            var retornaAluno = await db.QueryAsync<AlunoEntity>(sql: stringSql, param: parametros, commandType: CommandType.Text);
            return retornaAluno;
        }

        public async Task<int> RetornaMaxCodigoCadastroAsync()
        {
            string stringSql = $"SELECT MAX(CodigoCadastro) FROM Aluno a ";
            var retornaMax = await db.QueryFirstOrDefaultAsync<int>(sql: stringSql, commandType: CommandType.Text);
            return retornaMax;
        }
    }
}

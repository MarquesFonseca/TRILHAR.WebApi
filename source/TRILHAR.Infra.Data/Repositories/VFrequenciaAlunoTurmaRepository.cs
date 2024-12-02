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
    public class VFrequenciaAlunoTurmaRepository : RepositoryGenericsBase<SqlConnection, VFrequenciaAlunoTurmaEntity>, IVFrequenciaAlunoTurmaRepository
    {
        private readonly SqlConnection _conn;
        public VFrequenciaAlunoTurmaRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            _conn = sqlConnection;
        }

        protected override string ObterCampos() => "Codigo, DataFrequencia, CodigoAluno, CodigoTurma, Presenca, CodigoUsuarioLogado, DataAtualizacao, DataCadastro, AlunoCodigoCadastro, AlunoNomeCrianca, AlunoDataNascimento, AlunoNomeMae, AlunoNomePai, AlunoOutroResponsavel, AlunoTelefone, AlunoEnderecoEmail, AlunoAlergia, AlunoDescricaoAlergia, AlunoRestricaoAlimentar, AlunoDescricaoRestricaoAlimentar, AlunoDeficienciaOuSituacaoAtipica, AlunoDescricaoDeficiencia, AlunoBatizado, AlunoDataBatizado, AlunoIgrejaBatizado, AlunoAtivo, TurmaDescricao, TurmaIdadeInicialAluno, TurmaIdadeFinalAluno, TurmaAnoLetivo, TurmaSemestreLetivo, TurmaLimiteMaximo, TurmaAtivo";

        protected override string ObterTabela() => "VFrequenciaAlunoTurma";
    }
}

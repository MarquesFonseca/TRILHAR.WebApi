using System.Data.SqlClient;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;

namespace TRILHAR.Infra.Data.Repositories
{
    public class VFrequenciaRepository : RepositoryGenericsBase<SqlConnection, VFrequenciaEntity>, IVFrequenciaRepository
    {
        private readonly SqlConnection _conn;
        public VFrequenciaRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            _conn = sqlConnection;
        }

        protected override string ObterCampos() => "Codigo, DataFrequencia, CodigoAluno, CodigoTurma, Presenca, CodigoUsuarioLogado, DataAtualizacao, DataCadastro, AlunoCodigoCadastro, AlunoNomeCrianca, AlunoDataNascimento, AlunoNomeMae, AlunoNomePai, AlunoOutroResponsavel, AlunoTelefone, AlunoEnderecoEmail, AlunoAlergia, AlunoDescricaoAlergia, AlunoRestricaoAlimentar, AlunoDescricaoRestricaoAlimentar, AlunoDeficienciaOuSituacaoAtipica, AlunoDescricaoDeficiencia, AlunoBatizado, AlunoDataBatizado, AlunoIgrejaBatizado, AlunoAtivo, TurmaDescricao, TurmaIdadeInicialAluno, TurmaIdadeFinalAluno, TurmaAnoLetivo, TurmaSemestreLetivo, TurmaLimiteMaximo, TurmaAtivo";

        //protected override string ObterTabela() => "VFrequenciaAlunoTurma";
        protected override string ObterTabela() => "VFrequenciaCompleta";
    }
}

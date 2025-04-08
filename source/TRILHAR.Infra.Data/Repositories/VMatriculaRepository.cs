using System.Data.SqlClient;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Interfaces.Repositories;

namespace TRILHAR.Infra.Data.Repositories
{
    public class VMatriculaRepository : RepositoryGenericsBase<SqlConnection, VMatriculaEntity>, IVMatriculaRepository
    {
        private readonly SqlConnection _conn;
        public VMatriculaRepository(SqlConnection sqlConnection) : base(sqlConnection)
        {
            _conn = sqlConnection;
        }

        protected override string ObterCampos() => "Codigo, CodigoAluno, CodigoTurma, Ativo, CodigoUsuarioLogado, DataAtualizacao, DataCadastro, AlunoCodigoCadastro, AlunoNomeCrianca, AlunoDataNascimento, AlunoNomeMae, AlunoNomePai, AlunoOutroResponsavel, AlunoTelefone, AlunoEnderecoEmail, AlunoAlergia, AlunoDescricaoAlergia, AlunoRestricaoAlimentar, AlunoDescricaoRestricaoAlimentar, AlunoDeficienciaOuSituacaoAtipica, AlunoDescricaoDeficiencia, AlunoBatizado, AlunoDataBatizado, AlunoIgrejaBatizado, AlunoAtivo, TurmaDescricao, TurmaIdadeInicialAluno, TurmaIdadeFinalAluno, TurmaAnoLetivo, TurmaSemestreLetivo, TurmaLimiteMaximo, TurmaAtivo";

        protected override string ObterTabela() => "VMatriculaAlunoTurma";


    }
}

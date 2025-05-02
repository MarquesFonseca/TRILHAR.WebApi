using Dapper.Contrib.Extensions;

namespace TRILHAR.Business.Entities
{
    [Table("VFrequenciaAlunoTurma")]
    public class VFrequenciaEntity : EntityBase
    {
        public int Codigo { get; set; }
        public DateTime? DataFrequencia { get; set; }
        public int CodigoAluno { get; set; }
        public int CodigoTurma { get; set; }
        public bool Presenca { get; set; }
        public int CodigoUsuarioLogado { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCadastro { get; set; }

        public string AlunoCodigoCadastro { get; set; } = string.Empty;
        public string AlunoNomeCrianca { get; set; } = string.Empty;
        public DateTime? AlunoDataNascimento { get; set; }
        public string? AlunoNomeMae { get; set; }
        public string? AlunoNomePai { get; set; }
        public string? AlunoOutroResponsavel { get; set; }
        public string? AlunoTelefone { get; set; }
        public string? AlunoEnderecoEmail { get; set; }
        public bool AlunoAlergia { get; set; }
        public string? AlunoDescricaoAlergia { get; set; }
        public bool AlunoRestricaoAlimentar { get; set; }
        public string? AlunoDescricaoRestricaoAlimentar { get; set; }
        public bool AlunoDeficienciaOuSituacaoAtipica { get; set; }
        public string? AlunoDescricaoDeficiencia { get; set; }
        public bool AlunoBatizado { get; set; }
        public DateTime? AlunoDataBatizado { get; set; }
        public string? AlunoIgrejaBatizado { get; set; }
        public bool AlunoAtivo { get; set; }

        public string TurmaDescricao { get; set; } = string.Empty;
        public DateTime? TurmaIdadeInicialAluno { get; set; }
        public DateTime? TurmaIdadeFinalAluno { get; set; }
        public int TurmaAnoLetivo { get; set; }
        public int TurmaSemestreLetivo { get; set; }
        public bool TurmaAtivo { get; set; }
    }
}
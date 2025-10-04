namespace TRILHAR.Business.IO.VMatricula
{
    public class VMatriculaInput
    {
        public int? Codigo { get; set; }
        public int? CodigoAluno { get; set; }
        public int? CodigoTurma { get; set; }
        public bool? Ativo { get; set; }
        public int? CodigoUsuarioLogado { get; set; }
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

        public bool? AlunoAlergia { get; set; }
        public string? AlunoDescricaoAlergia { get; set; }
        public bool? AlunoRestricaoAlimentar { get; set; }
        public string? AlunoDescricaoRestricaoAlimentar { get; set; }
        public bool? AlunoDeficienciaOuSituacaoAtipica { get; set; }
        public string? AlunoDescricaoDeficiencia { get; set; }
        public bool? AlunoBatizado { get; set; }
        public DateTime? AlunoDataBatizado { get; set; }
        public string? AlunoIgrejaBatizado { get; set; }
        public bool? AlunoAtivo { get; set; }

        public string TurmaDescricao { get; set; } = string.Empty;
        public DateTime? TurmaIdadeInicialAluno { get; set; }
        public DateTime? TurmaIdadeFinalAluno { get; set; }
        public int? TurmaAnoLetivo { get; set; }
        public int? TurmaSemestreLetivo { get; set; }
        public bool? TurmaAtivo { get; set; }


        public DateTime? DataNascimentoInicial { get; set; }
        public DateTime? DataNascimentoFinal { get; set; }
        public DateTime? DataBatizadoInicial { get; set; }
        public DateTime? DataBatizadoFinal { get; set; }
        public DateTime? DataAtualizacaoInicial { get; set; }
        public DateTime? DataAtualizacaoFinal { get; set; }
        public DateTime? DataCadastroInicial { get; set; }
        public DateTime? DataCadastroFinal { get; set; }
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public bool isPaginacao { get; set; } = true;
    }
}

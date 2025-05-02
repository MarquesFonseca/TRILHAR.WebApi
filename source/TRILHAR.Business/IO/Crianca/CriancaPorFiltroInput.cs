namespace TRILHAR.Business.IO.Crianca
{
    public class CriancaPorFiltroInput
    {
        public int? Codigo { get; set; } = 0;
        public string? CodigoCadastro { get; set; }
        public string? NomeCrianca { get; set; }
        public DateTime? DataNascimento { get; set; }
        public DateTime? DataNascimentoInicial { get; set; }
        public DateTime? DataNascimentoFinal { get; set; }
        public string? NomeMae { get; set; }
        public string? NomePai { get; set; }
        public string? OutroResponsavel { get; set; }
        public bool? Alergia { get; set; }
        public bool? RestricaoAlimentar { get; set; }
        public bool? DeficienciaOuSituacaoAtipica { get; set; }
        public bool? Batizado { get; set; }
        public DateTime? DataBatizadoInicial { get; set; }
        public DateTime? DataBatizadoFinal { get; set; }
        public DateTime? DataAtualizacaoInicial { get; set; }
        public DateTime? DataAtualizacaoFinal { get; set; }
        public DateTime? DataCadastroInicial { get; set; }
        public DateTime? DataCadastroFinal { get; set; }
        public bool? Ativo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool IsPaginacao { get; set; } = true;
    }
}

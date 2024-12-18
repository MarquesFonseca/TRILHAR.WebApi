using Dapper.Contrib.Extensions;

namespace TRILHAR.Business.Entities
{
    [Table("Pagina")]
    public class PaginaEntity : EntityBase
    {
        [Key]
        public int Codigo { get; set; }
        public string? Descricao { get; set; }
        public string? Rota { get; set; }
        public int? CodigoUsuarioCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}



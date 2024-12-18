using Dapper.Contrib.Extensions;

namespace TRILHAR.Business.Entities
{
    [Table("Permissao")]
    public class PermissaoEntity : EntityBase
    {
        [Key]
        public int Codigo { get; set; }
        public int CodigoUsuario { get; set; }
        public int CodigoPagina { get; set; }
        public bool Acesso { get; set; }
        public int? CodigoUsuarioCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}



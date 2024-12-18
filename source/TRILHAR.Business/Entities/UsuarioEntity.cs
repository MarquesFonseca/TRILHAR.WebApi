using Dapper.Contrib.Extensions;

namespace TRILHAR.Business.Entities
{
    [Table("Usuario")]
    public class UsuarioEntity : EntityBase
    {
        [Key]
        public int Codigo { get; set; }
        public string? Cpf { get; set; }
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public string? TelefoneAlternativo { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public int DiasExpiracao { get; set; }
        public int NivelConsulta { get; set; }
        public int NivelAtualizacao { get; set; }
        public DateTime? DataUltimoAcesso { get; set; }
        public int NuTentativa { get; set; }
        public bool Ativo { get; set; }
        public int? CodigoUsuarioCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public int CodigoDepartamento { get; set; }
        public string? NomeFuncao { get; set; }
    }
}



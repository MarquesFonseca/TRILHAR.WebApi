using System;
using Dapper.Contrib.Extensions;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace TRILHAR.Business.Entities
{
    [Table("Turma")]
    public class TurmaEntity : EntityBase
    {
        [Key]
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public DateTime? IdadeInicialAluno { get; set; }
        public DateTime? IdadeFinalAluno { get; set; }
        public int AnoLetivo { get; set; }
        public int SemestreLetivo { get; set; }
        public int? LimiteMaximo { get; set; }
        public bool Ativo { get; set; }
        public int? CodigoUsuarioLogado { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}

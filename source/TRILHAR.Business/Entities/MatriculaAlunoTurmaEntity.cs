using System;
using Dapper.Contrib.Extensions;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace TRILHAR.Business.Entities
{
    [Table("MatriculaAlunoTurma")]
    public class MatriculaAlunoTurmaEntity : EntityBase
    {
        [Key]
        public int Codigo { get; set; }
        public int CodigoAluno { get; set; }
        public int CodigoTurma { get; set; }
        public bool Ativo { get; set; }
        public int? CodigoUsuarioLogado { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}

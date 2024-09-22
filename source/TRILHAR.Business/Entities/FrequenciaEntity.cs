using System;
using Dapper.Contrib.Extensions;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace TRILHAR.Business.Entities
{
    [Table("Frequencia")]
    public class FrequenciaEntity : EntityBase
    {
        [Key]
        public int Codigo { get; set; }
        public DateTime? DataFrequencia { get; set; }
        public int CodigoAluno { get; set; }
        public int CodigoTurma { get; set; }
        public bool Presenca { get; set; }

        public string TurmaDescricao { get; set; }
        public DateTime? TurmaIdadeInicialAluno { get; set; }
        public DateTime? TurmaIdadeFinalAluno { get; set; }
        public int? TurmaAnoLetivo { get; set; }
        public int? TurmaSemestreLetivo { get; set; }

        public int? CodigoUsuarioLogado { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
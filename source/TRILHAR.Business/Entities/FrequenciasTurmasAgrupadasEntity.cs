using System;
using Dapper.Contrib.Extensions;

namespace TRILHAR.Business.Entities
{
    [Table("SPFrequenciasTodasTurmasAgrupadasDia")]
    public class FrequenciasTurmasAgrupadasEntity : EntityBase
    {
        [Key]
        public DateTime DataFrequencia { get; set; }
        public int CodigoTurma { get; set; } = 0;
        public string TurmaDescricao { get; set; } = string.Empty;
        public int TurmaAnoLetivo { get; set; } = 0;
        public int TurmaSemestreLetivo { get; set; } = 0;
        public DateTime TurmaIdadeInicialAluno { get; set; }
        public DateTime TurmaIdadeFinalAluno { get; set; }
        public bool TurmaAtivo { get; set; }
        public int TurmaLimiteMaximo { get; set; } = 0;
        public int Qtd { get; set; } = 0;
    }
}
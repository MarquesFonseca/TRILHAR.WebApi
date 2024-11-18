using System;
using System.Collections.Generic;
using System.Text;

namespace TRILHAR.Business.IO.Frequencia
{
    public class FrequenciaInput
    {
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

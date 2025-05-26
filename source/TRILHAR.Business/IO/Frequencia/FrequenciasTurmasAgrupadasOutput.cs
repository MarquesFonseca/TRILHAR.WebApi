using TRILHAR.Business.Entities;

namespace TRILHAR.Business.IO.Frequencia
{
    public class FrequenciasTurmasAgrupadasOutput : FrequenciasTurmasAgrupadasEntity
    {
        public string DataFrequenciaFormatada { get; set; } = string.Empty;
        public string TurmaDescricaoFormatada { get; set; } = string.Empty;
        public string TurmaIdadeInicialAlunoFormatada { get; set; } = string.Empty;
        public string TurmaIdadeFinalAlunoFormatada { get; set; } = string.Empty;
        public int QtdRestante { get; set; } = 0;
        public string QtdRestanteFormatada { get; set; } = string.Empty;
        public int QtdAlergia { get; set; } = 0;
        public int QtdRestricaoAlimentar { get; set; } = 0;
        public int QtdNecessidadesEspeciais { get; set; } = 0;
        public int QtdAniversariantes { get; set; } = 0;
    }
}

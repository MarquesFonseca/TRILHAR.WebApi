using TRILHAR.Business.Entities;

namespace TRILHAR.Business.IO.Aluno
{
    public class AlunoInput : AlunoEntity
    {
        public DateTime? DataNascimentoInicial { get; set; }
        public DateTime? DataNascimentoFinal { get; set; }
        public DateTime? DataBatizadoInicial { get; set; }
        public DateTime? DataBatizadoFinal { get; set; }
        public DateTime? DataAtualizacaoInicial { get; set; }
        public DateTime? DataAtualizacaoFinal { get; set; }
        public DateTime? DataCadastroInicial { get; set; }
        public DateTime? DataCadastroFinal { get; set; }

    }
}

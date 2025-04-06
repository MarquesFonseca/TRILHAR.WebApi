using TRILHAR.Business.Entities;

namespace TRILHAR.Business.IO.Turma
{
    public class TurmaOutput : TurmaEntity
    {
        public string DescricaoAnoSemestreLetivo => $"{Descricao} - {AnoLetivo}/{SemestreLetivo}";
    }
}

using TRILHAR.Business.Entities;

namespace TRILHAR.Business.IO.Aluno
{
    public class AlunoOutput : AlunoEntity
    {
        public MatriculaEntity? Matricula { get; set; } = null;
    }
}

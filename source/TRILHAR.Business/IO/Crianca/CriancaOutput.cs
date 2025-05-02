using TRILHAR.Business.Entities;

namespace TRILHAR.Business.IO.Crianca
{
    public class CriancaOutput : CriancaEntity
    {
        public MatriculaEntity? Matricula { get; set; } = null;
    }
}

using TRILHAR.Business.Entities;

namespace TRILHAR.Business.IO.Crianca
{
    public class CriancaOutput : CriancaEntity
    {
        public VMatriculaEntity? Matricula { get; set; } = null;
    }
}

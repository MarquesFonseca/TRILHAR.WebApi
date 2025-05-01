using TRILHAR.Business.IO.Matricula;

namespace TRILHAR.Business.Interfaces.Services
{
    public interface IVMatriculaService : IDisposable
    {
        Task<IEnumerable<VMatriculaOutput>?> ListarPorCodigoAlunoCodigoTurma(int codigoAluno, int codigoTurma);
        Task<IEnumerable<VMatriculaOutput>?> ListarPorCodigoAluno(int codigoAluno);
        Task<IEnumerable<VMatriculaOutput>?> ListarPorCodigoTurma(int codigoTurma);
    }
}

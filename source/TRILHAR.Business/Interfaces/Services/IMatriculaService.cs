using TRILHAR.Business.IO.Matricula;

namespace TRILHAR.Business.Interfaces.Services
{
    public interface IMatriculaService : IDisposable
    {
        Task<IEnumerable<MatriculaOutput>?> ListarPorCodigoAlunoCodigoTurma(int codigoAluno, int codigoTurma);
        Task<IEnumerable<MatriculaOutput>?> ListarPorCodigoAluno(int codigoAluno);
        Task<IEnumerable<MatriculaOutput>?> ListarPorCodigoTurma(int codigoTurma);
        Task<int> InsertAsync(MatriculaInput entity);
        Task<int> InsertAsync(IEnumerable<MatriculaInput> list);
        Task<bool> UpdateAsync(MatriculaInput entity);
        Task<bool> UpdateAsync(IEnumerable<MatriculaInput> list);
    }
}

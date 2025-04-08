using TRILHAR.Business.IO.Matricula;

namespace TRILHAR.Business.Interfaces.Services
{
    public interface IMatriculaService : IDisposable
    {
        Task<int> InsertAsync(MatriculaInput entity);
        Task<int> InsertAsync(IEnumerable<MatriculaInput> list);
        Task<bool> UpdateAsync(MatriculaInput entity);
        Task<bool> UpdateAsync(IEnumerable<MatriculaInput> list);
    }
}

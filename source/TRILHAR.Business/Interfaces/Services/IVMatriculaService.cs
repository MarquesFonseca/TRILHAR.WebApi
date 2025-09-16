using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Matricula;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.Interfaces.Services
{
    public interface IVMatriculaService : IServiceGenericsBase<VMatriculaEntity>
    {
        Task<IEnumerable<VMatriculaOutput>?> ListarPorCodigoAlunoCodigoTurma(int codigoAluno, int codigoTurma);
        Task<IEnumerable<VMatriculaOutput>?> ListarPorCodigoAluno(int codigoAluno);
        Task<IEnumerable<VMatriculaOutput>?> ListarPorCodigoTurma(int codigoTurma);
        Task<PagedResult<VMatriculaOutput>> GetByListarPorFiltroPaginacaoAsync(VMatriculaInput input);
    }
}
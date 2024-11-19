using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Aluno;

namespace TRILHAR.Business.Interfaces.Services
{
    public interface IAlunoService : IServiceGenericsBase<AlunoEntity>
    {
        Task<int> InsertAsync(AlunoInput entity);
        Task<int> InsertAsync(IEnumerable<AlunoInput> list);
        Task<bool> UpdateAsync(AlunoInput entity);
        Task<bool> UpdateAsync(IEnumerable<AlunoInput> list);
    }
}

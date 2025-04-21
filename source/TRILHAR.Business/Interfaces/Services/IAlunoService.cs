using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Aluno;
using TRILHAR.Business.IO.Paginacao;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.Interfaces.Services
{
    public interface IAlunoService : IServiceGenericsBase<AlunoEntity>
    {
        Task<AlunoOutput?> GetByCodigoCadastroAsync(string codigoCadastro);
        Task<PagedResult<AlunoOutput>> GetByListarPorFiltroPaginacaoAsync(AlunoInput input);
        Task<int> InsertAsync(AlunoInput entity);
        Task<int> InsertAsync(IEnumerable<AlunoInput> list);
        Task<bool> UpdateAsync(AlunoInput entity);
        Task<bool> UpdateAsync(IEnumerable<AlunoInput> list);
    }
}

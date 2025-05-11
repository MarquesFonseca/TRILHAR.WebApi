using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Crianca;
using TRILHAR.Business.IO.Paginacao;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.Interfaces.Services
{
    public interface ICriancaService : IServiceGenericsBase<CriancaEntity>
    {
        Task<CriancaOutput?> GetByCodigoAsync(int codigo);
        Task<CriancaOutput?> GetByCodigoCadastroAsync(string codigoCadastro);
        Task<PagedResult<CriancaOutput>> GetByListarPorFiltroPaginacaoAsync(CriancaInput input);
        Task<int> InsertAsync(CriancaInput entity);
        Task<int> InsertAsync(IEnumerable<CriancaInput> list);
        Task<bool> UpdateAsync(CriancaInput entity);
        Task<bool> UpdateAsync(IEnumerable<CriancaInput> list);
    }
}

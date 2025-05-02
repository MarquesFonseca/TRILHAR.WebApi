using TRILHAR.Business.Entities;

namespace TRILHAR.Business.Interfaces.Repositories
{
    public interface ICriancaRepository : IRepositoryGenericsBase<CriancaEntity>
    {
        Task<CriancaEntity?> GetByCodigoCadastroAsync(string codigoCadastro);
        Task<int> GetMaxCodigoCadastroAsync();
        Task<int> InsertOutputInsertedAsync(CriancaEntity entity);
        Task<int> UpdateRegistroAsync(CriancaEntity entity);
    }
}

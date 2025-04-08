using TRILHAR.Business.Entities;

namespace TRILHAR.Business.Interfaces.Repositories
{
    public interface IAlunoRepository : IRepositoryGenericsBase<AlunoEntity>
    {
        Task<AlunoEntity?> GetByCodigoCadastroAsync(string codigoCadastro);
        Task<int> GetMaxCodigoCadastroAsync();
        Task<int> InsertOutputInsertedAsync(AlunoEntity entity);
        Task<int> UpdateRegistroAsync(AlunoEntity entity);
    }
}

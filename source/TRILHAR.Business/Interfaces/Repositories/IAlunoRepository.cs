using TRILHAR.Business.Entities;

namespace TRILHAR.Business.Interfaces.Repositories
{
    public interface IAlunoRepository : IRepositoryGenericsBase<AlunoEntity>
    {
        Task<AlunoEntity?> RetornaByCodigoCadastroAsync(string codigoCadastro);
        Task<int> RetornaMaxCodigoCadastroAsync();
        Task<int> InsertOutputInsertedAsync(AlunoEntity entity);
        Task<int> AtualizarRegistroAsync(AlunoEntity entity);
    }
}

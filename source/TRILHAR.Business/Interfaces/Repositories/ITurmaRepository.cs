using TRILHAR.Business.Entities;

namespace TRILHAR.Business.Interfaces.Repositories
{
    public interface ITurmaRepository: IRepositoryGenericsBase<TurmaEntity>
    {
        Task<int> InsertOutputInsertedAsync(TurmaEntity entity);
    }
}

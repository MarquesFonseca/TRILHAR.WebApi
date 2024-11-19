using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TRILHAR.Business.Entities;

namespace TRILHAR.Business.Interfaces.Repositories
{
    public interface ITurmaRepository: IRepositoryGenericsBase<TurmaEntity>
    {
        Task<int> InsertOutputInsertedAsync(AlunoEntity entity);
    }
}

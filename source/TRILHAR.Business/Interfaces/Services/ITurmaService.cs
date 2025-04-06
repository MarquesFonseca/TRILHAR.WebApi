using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Turma;

namespace TRILHAR.Business.Interfaces.Services
{
    public interface ITurmaService : IDisposable
    {
        Task<IEnumerable<TurmaOutput>> ListarTurmasAtivas();
    }
}

using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Aluno;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.Interfaces.Services
{
    public interface IAlunoService : IDisposable
    {
        //Task<PagedResult<IEnumerable<AlunoEntity>>> RetornaAllAsync(bool isPaginacao = false, int page = 1, int pageSize = 10);
        //Task<AlunoEntity> RetornaByCodigoAsync(int codigo);
        Task<int> NovoRegistroAsync(AlunoInput entity);
        Task<int> AtualizarRegistroAsync(AlunoInput entity);
        Task<int> DeletarRegistroAsync(int codigo);
        Task<AlunoEntity> RetornaByCodigoCadastroAsync(string codigoCadastro);
        Task<AlunoEntity> RetornaByCondicaoAsync(string condicao, object parametros);
        Task<IEnumerable<AlunoEntity>> RetornaListaByCondicaoAsync(string condicao, object parametros);
        Task<int> RetornaMaxCodigoCadastroAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TRILHAR.Business.Entities;

namespace TRILHAR.Business.Interfaces.Repositories
{
    public interface IAlunoRepository : IRepositoryGenericsBase<AlunoEntity>
    {
        Task<IEnumerable<AlunoEntity>> RetornaAllAsync(bool isPaginacao = false, int page = 1, int pageSize = 10);
        Task<AlunoEntity> RetornaByCodigoAsync(int codigo);
        Task<int> NovoRegistroAsync(AlunoEntity entity);
        Task<int> AtualizarRegistroAsync(AlunoEntity entity);
        Task<int> DeletarRegistroAsync(int codigo);
        Task<AlunoEntity> RetornaByCodigoCadastroAsync(string codigoCadastro);
        Task<AlunoEntity> RetornaByCondicaoAsync(string condicao, object parametros);
        Task<IEnumerable<AlunoEntity>> RetornaListaByCondicaoAsync(string condicao, object parametros);
        Task<int> RetornaMaxCodigoCadastroAsync();
    }
}

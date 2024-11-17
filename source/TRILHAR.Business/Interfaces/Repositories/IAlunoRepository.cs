using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Paginacao;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.Interfaces.Repositories
{
    public interface IAlunoRepository : IRepositoryGenericsBase<AlunoEntity>
    {
        Task<AlunoEntity?> RetornaByCodigoCadastroAsync(string codigoCadastro);
        Task<int> RetornaMaxCodigoCadastroAsync();
        Task<int> NovoRegistroAsync(AlunoEntity entity);
        Task<int> AtualizarRegistroAsync(AlunoEntity entity);
        Task<int> DeletarRegistroAsync(int codigo);
    }
}

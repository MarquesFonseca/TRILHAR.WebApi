using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TRILHAR.Business.Entities;

namespace TRILHAR.Business.Interfaces.Repositories
{
    public interface IAlunoRepository : IRepositoryGenericsBase<AlunoEntity>
    {
        Task<IEnumerable<AlunoEntity>> RetornaAll();
        Task<AlunoEntity> RetornaByCodigo(int codigo);
        Task<int> NovoRegistro(AlunoEntity entity);
        Task<int> AtualizarRegistro(AlunoEntity entity);
        Task<int> DeletarRegistro(int codigo);
        Task<AlunoEntity> RetornaByCodigoCadastro(string codigoCadastro);
        Task<AlunoEntity> RetornaByCondicao(string condicao, object parametros);
        Task<IEnumerable<AlunoEntity>> RetornaListaByCondicao(string condicao, object parametros);
        Task<int> RetornaMaxCodigoCadastro();
    }
}

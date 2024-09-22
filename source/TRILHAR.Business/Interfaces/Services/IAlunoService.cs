using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Aluno;

namespace TRILHAR.Business.Interfaces.Services
{
    public interface IAlunoService : IDisposable
    {
        Task<IEnumerable<AlunoEntity>> RetornaAll();
        Task<AlunoEntity> RetornaByCodigo(int codigo);
        Task<int> NovoRegistro(AlunoInput entity);
        Task<int> AtualizarRegistro(AlunoInput entity);
        Task<int> DeletarRegistro(int codigo);
        Task<AlunoEntity> RetornaByCodigoCadastro(string codigoCadastro);
        Task<AlunoEntity> RetornaByCondicao(string condicao, object parametros);
        Task<IEnumerable<AlunoEntity>> RetornaListaByCondicao(string condicao, object parametros);
        Task<int> RetornaMaxCodigoCadastro();
    }
}

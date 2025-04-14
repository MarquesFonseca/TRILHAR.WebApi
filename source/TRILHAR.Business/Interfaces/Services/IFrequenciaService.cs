using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Frequencia;
using TRILHAR.Business.IO.Paginacao;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.Interfaces.Services
{
    public interface IFrequenciaService : IServiceGenericsBase<FrequenciaEntity>
    {
        Task<int> InsertAsync(FrequenciaInput entity);
        Task<int> InsertAsync(IEnumerable<FrequenciaInput> list);
        Task<IEnumerable<FrequenciaOutput>> ListarPorCodigoAlunoCodigoTurma(int codigoAluno, int codigoTurma);
        Task<IEnumerable<FrequenciaOutput>> ListarPorCodigoAluno(int codigoAluno);
        Task<IEnumerable<FrequenciaOutput>> ListarPorCodigoTurma(int codigoTurma);
        Task<bool> UpdateAsync(FrequenciaInput entity);
        Task<bool> UpdateAsync(IEnumerable<FrequenciaInput> list);
    }
}

using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Frequencia;

namespace TRILHAR.Business.Interfaces.Services
{
    public interface IFrequenciaService : IServiceGenericsBase<FrequenciaEntity>
    {
        Task<IEnumerable<FrequenciaOutput>?> ListarPorCodigoAlunoCodigoTurma(int codigoAluno, int codigoTurma);
        Task<IEnumerable<FrequenciaOutput>?> ListarPorCodigoAluno(int codigoAluno);
        Task<IEnumerable<FrequenciaOutput>?> ListarPorCodigoTurma(int codigoTurma);
        Task<IEnumerable<dynamic>?> GetFrequenciasAlunosPorDataFrequencia(DateTime dataFrequencia);
        Task<IEnumerable<dynamic>?> GetFrequenciasTurmasAgrupadasPorDataFrequencia(DateTime dataFrequencia);
        Task<int> InsertAsync(FrequenciaInput entity);
        Task<int> InsertAsync(IEnumerable<FrequenciaInput> list);
        Task<bool> UpdateAsync(FrequenciaInput entity);
        Task<bool> UpdateAsync(IEnumerable<FrequenciaInput> list);
    }
}

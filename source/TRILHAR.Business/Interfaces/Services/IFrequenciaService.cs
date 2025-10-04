using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Frequencia;

namespace TRILHAR.Business.Interfaces.Services
{
    public interface IFrequenciaService : IServiceGenericsBase<FrequenciaEntity>
    {
        Task<int> AddAsync(FrequenciaInput input);
        Task<int> AddManyAsync(IEnumerable<FrequenciaInput> inputs);
        Task<bool> UpdateAsync(FrequenciaInput input);
        Task<bool> UpdateManyAsync(IEnumerable<FrequenciaInput> inputs);
    }
}

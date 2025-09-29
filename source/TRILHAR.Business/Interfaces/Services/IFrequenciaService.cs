using TRILHAR.Business.Entities;
using TRILHAR.Business.Enums;
using TRILHAR.Business.IO.Frequencia;
using TRILHAR.Business.IO.Matricula;

namespace TRILHAR.Business.Interfaces.Services
{
    public interface IFrequenciaService : IServiceGenericsBase<FrequenciaEntity>
    {
        Task<int> AddAsync(FrequenciaInput input);
        Task<int> AddManyAsync(IEnumerable<FrequenciaInput> inputs);

        Task<bool> UpdateAsync(FrequenciaInput input);
        Task<bool> UpdateManyAsync(IEnumerable<FrequenciaInput> inputs);
        
        //3 - "{data}"
        Task<IEnumerable<VFrequenciaOutput>?> GetByDateAsync(DateTime dataFrequencia);//SPFrequenciasPorData @DataFrequencia

        //4 - "turmas/agrupadas/{data}
        Task<IEnumerable<FrequenciasTurmasAgrupadasOutput>?> GetTurmasAgrupadasByDateAsync(DateTime dataFrequencia);//SPFrequenciasTodasTurmasAgrupadasDia @DataFrequencia

        //5 - "turmas/{codigoTurma}/{data}"
        Task<IEnumerable<VFrequenciaOutput>?> GetByTurmasAndDateAsync(int codigoTurma, DateTime dataFrequencia);

        //6
        Task<IEnumerable<VFrequenciaOutput>?> GetByAlunoAsync(int codigoAluno);
        
        //7
        Task<IEnumerable<VFrequenciaOutput>?> GetByTurmaAsync(int codigoTurma);
        
        //8
        Task<IEnumerable<VFrequenciaOutput>?> GetByAlunoAndTurmaAsync(int codigoAluno, int codigoTurma);

        //9
        Task<IEnumerable<VFrequenciaOutput>?> GetByAlunoAndTurmaAndDateAsync(int codigoAluno, int codigoTurma, DateTime dataFrequencia);

    }
}

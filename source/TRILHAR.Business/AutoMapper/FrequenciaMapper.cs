using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Entities.Trilhar;
using TRILHAR.Business.Entities.Trilhar.ViewModel;
using TRILHAR.Business.IO.Aluno;
using TRILHAR.Business.IO.Frequencia;
using TRILHAR.Business.IO.Turma;

namespace TRILHAR.Business.AutoMapper
{
    public class FrequenciaMapper : Profile
    {
        public FrequenciaMapper()
        {
            CreateMap<FrequenciaEntity, FrequenciaInput>()
                .ReverseMap();
            
            CreateMap<FrequenciaEntity, FrequenciaOutput>()
                .ReverseMap();

            CreateMap<FrequenciaInput, FrequenciaEntity>()
                .ReverseMap();

            CreateMap<FrequenciaOutput, FrequenciaEntity>()
                .ReverseMap();
        }
    }
}

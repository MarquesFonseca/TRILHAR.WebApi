using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Frequencia;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.AutoMapper
{
    public class FrequenciaMapper : Profile
    {
        public FrequenciaMapper()
        {
            CreateMap<FrequenciaEntity, FrequenciaInput>().ReverseMap();
            CreateMap<FrequenciaEntity, FrequenciaOutput>().ReverseMap();
            CreateMap<FrequenciaInput, FrequenciaOutput>().ReverseMap();
            CreateMap<PagedResult<FrequenciaEntity>, PagedResult<FrequenciaOutput>>().ReverseMap();
        }
    }
}

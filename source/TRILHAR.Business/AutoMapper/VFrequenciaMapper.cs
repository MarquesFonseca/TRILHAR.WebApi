using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.VFrequencia;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.AutoMapper
{
    public class VFrequenciaMapper : Profile
    {
        public VFrequenciaMapper()
        {
            CreateMap<VFrequenciaEntity, VFrequenciaInput>().ReverseMap();
            CreateMap<VFrequenciaEntity, VFrequenciaOutput>().ReverseMap();
            CreateMap<VFrequenciaInput, VFrequenciaOutput>().ReverseMap();
            CreateMap<PagedResult<VFrequenciaEntity>, PagedResult<VFrequenciaOutput>>().ReverseMap();
        }
    }
}

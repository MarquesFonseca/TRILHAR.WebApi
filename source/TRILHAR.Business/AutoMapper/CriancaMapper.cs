using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Crianca;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.AutoMapper
{
    public class CriancaMapper : Profile
    {
        public CriancaMapper()
        {
            CreateMap<CriancaEntity, CriancaInput>().ReverseMap();
            CreateMap<CriancaEntity, CriancaOutput>().ReverseMap();
            CreateMap<CriancaInput, CriancaOutput>().ReverseMap();
            CreateMap<PagedResult<CriancaEntity>, PagedResult<CriancaOutput>>().ReverseMap();
            CreateMap<CriancaInput, CriancaPorFiltroInput>().ReverseMap();

        }
    }
}

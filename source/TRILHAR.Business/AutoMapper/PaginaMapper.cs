using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Pagina;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.AutoMapper
{
    public class PaginaMapper : Profile
    {
        public PaginaMapper()
        {
            CreateMap<PaginaEntity, PaginaInput>().ReverseMap();
            CreateMap<PaginaEntity, PaginaOutput>().ReverseMap();
            CreateMap<PaginaInput, PaginaOutput>().ReverseMap();
            CreateMap<PagedResult<PaginaEntity>, PagedResult<PaginaOutput>>().ReverseMap();
        }
    }
}

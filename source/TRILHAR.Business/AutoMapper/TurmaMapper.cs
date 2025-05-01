using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Turma;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.AutoMapper
{
    public class TurmaMapper : Profile
    {
        public TurmaMapper()
        {
            CreateMap<TurmaEntity, TurmaInput>().ReverseMap();
            CreateMap<TurmaEntity, TurmaOutput>().ReverseMap();
            CreateMap<TurmaInput, TurmaOutput>().ReverseMap();
            CreateMap<PagedResult<TurmaEntity>, PagedResult<TurmaOutput>>().ReverseMap();
        }
    }
}

using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Aluno;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.AutoMapper
{
    public class AlunoMapper : Profile
    {
        public AlunoMapper()
        {
            CreateMap<AlunoEntity, AlunoInput>().ReverseMap();
            CreateMap<AlunoEntity, AlunoOutput>().ReverseMap();
            CreateMap<AlunoInput, AlunoOutput>().ReverseMap();
            CreateMap<PagedResult<AlunoEntity>, PagedResult<AlunoOutput>>().ReverseMap();
            CreateMap<AlunoInput, AlunoPorFiltroInput>().ReverseMap();

        }
    }
}

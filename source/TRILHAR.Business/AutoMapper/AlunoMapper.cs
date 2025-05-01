using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Aluno;

namespace TRILHAR.Business.AutoMapper
{
    public class AlunoMapper : Profile
    {
        public AlunoMapper()
        {
            CreateMap<AlunoEntity, AlunoInput>().ReverseMap();
            CreateMap<AlunoEntity, AlunoOutput>().ReverseMap();
            CreateMap<AlunoInput, AlunoOutput>().ReverseMap();
            CreateMap<AlunoInput, AlunoPorFiltroInput>().ReverseMap();
        }
    }
}

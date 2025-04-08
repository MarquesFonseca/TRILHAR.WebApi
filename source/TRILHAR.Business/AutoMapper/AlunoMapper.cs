using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Entities.Trilhar;
using TRILHAR.Business.Entities.Trilhar.ViewModel;
using TRILHAR.Business.IO.Aluno;
using TRILHAR.Business.IO.Turma;

namespace TRILHAR.Business.AutoMapper
{
    public class AlunoMapper : Profile
    {
        public AlunoMapper()
        {
            CreateMap<AlunoEntity, AlunoInput>()
                .ReverseMap();

            CreateMap<AlunoEntity, AlunoOutput>()
                .ReverseMap();

            CreateMap<AlunoInput, AlunoEntity>()
                .ReverseMap();

            CreateMap<AlunoOutput, AlunoEntity>()
                .ReverseMap();
        }
    }
}

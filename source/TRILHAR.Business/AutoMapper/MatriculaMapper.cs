using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.Entities.Trilhar;
using TRILHAR.Business.Entities.Trilhar.ViewModel;
using TRILHAR.Business.IO.Aluno;
using TRILHAR.Business.IO.Matricula;
using TRILHAR.Business.IO.Turma;

namespace TRILHAR.Business.AutoMapper
{
    public class MatriculaMapper : Profile
    {
        public MatriculaMapper()
        {
            CreateMap<MatriculaEntity, MatriculaInput>()
                .ReverseMap();

            CreateMap<MatriculaEntity, MatriculaOutput>()
                .ReverseMap();

            CreateMap<MatriculaInput, MatriculaEntity>()
                .ReverseMap();

            CreateMap<MatriculaOutput, MatriculaEntity>()
                .ReverseMap();
        }
    }
}

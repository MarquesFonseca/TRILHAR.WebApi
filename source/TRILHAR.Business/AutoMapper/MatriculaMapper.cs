using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Matricula;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.AutoMapper
{
    public class MatriculaMapper : Profile
    {
        public MatriculaMapper()
        {
            CreateMap<MatriculaEntity, MatriculaInput>().ReverseMap();
            CreateMap<MatriculaEntity, MatriculaOutput>().ReverseMap();
            CreateMap<MatriculaInput, MatriculaOutput>().ReverseMap();
            CreateMap<PagedResult<MatriculaEntity>, PagedResult<MatriculaOutput>>().ReverseMap();
        }
    }
}

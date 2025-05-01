using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Matricula;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.AutoMapper
{
    public class VMatriculaMapper : Profile
    {
        public VMatriculaMapper()
        {
            CreateMap<VMatriculaEntity, VMatriculaInput>().ReverseMap();
            CreateMap<VMatriculaEntity, VMatriculaOutput>().ReverseMap();
            CreateMap<VMatriculaInput, VMatriculaOutput>().ReverseMap();
            CreateMap<PagedResult<VMatriculaEntity>, PagedResult<VMatriculaOutput>>().ReverseMap();
        }
    }
}

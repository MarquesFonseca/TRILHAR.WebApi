using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Usuario;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.AutoMapper
{
    public class UsuarioMapper : Profile
    {
        public UsuarioMapper()
        {
            CreateMap<UsuarioEntity, UsuarioInput>().ReverseMap();
            CreateMap<UsuarioEntity, UsuarioOutput>().ReverseMap();
            CreateMap<UsuarioInput, UsuarioOutput>().ReverseMap();
            CreateMap<PagedResult<UsuarioEntity>, PagedResult<UsuarioOutput>>().ReverseMap();

        }
    }
}

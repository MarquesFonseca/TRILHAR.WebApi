using AutoMapper;
using TRILHAR.Business.Entities;
using TRILHAR.Business.IO.Permissao;
using TRILHAR.Business.Pagination;

namespace TRILHAR.Business.AutoMapper
{
    public class PermissaoMapper : Profile
    {
        public PermissaoMapper()
        {
            CreateMap<PermissaoEntity, PermissaoInput>().ReverseMap();
            CreateMap<PermissaoEntity, PermissaoOutput>().ReverseMap();
            CreateMap<PermissaoInput, PermissaoOutput>().ReverseMap();
            CreateMap<PagedResult<PermissaoEntity>, PagedResult<PermissaoOutput>>().ReverseMap();

        }
    }
}

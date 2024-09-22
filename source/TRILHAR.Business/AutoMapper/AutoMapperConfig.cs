using AutoMapper;

namespace TRILHAR.Business.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
                cfg.AddProfile(new UsuarioSiggoMapper());
            });
        }
    }

}

using AutoMapper;

namespace TRILHAR.Business.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AlunoMapper());
                cfg.AddProfile(new AutoMapperProfile());
                cfg.AddProfile(new FrequenciaMapper());
                cfg.AddProfile(new MatriculaMapper());
                cfg.AddProfile(new PaginaMapper());
                cfg.AddProfile(new PermissaoMapper());
                cfg.AddProfile(new TurmaMapper());
                cfg.AddProfile(new UsuarioMapper());
                cfg.AddProfile(new UsuarioSiggoMapper());
                cfg.AddProfile(new VFrequenciaMapper());
                cfg.AddProfile(new VMatriculaMapper());
            });
        }
    }

}

using AutoMapper;
using TRILHAR.Business.Entities.Trilhar;
using TRILHAR.Business.Entities.Trilhar.ViewModel;

namespace TRILHAR.Business.AutoMapper
{
    public class UsuarioSiggoMapper : Profile
    {
        public UsuarioSiggoMapper()
        {
            CreateMap<UsuarioSistema, UsuarioSistemaViewModel>()
                .ForMember(d => d.Cpf, opt => opt.MapFrom(s => s.NUUSUARIO.ToString().PadLeft(11, '0')))
                .ForMember(d => d.CodigoSistema, opt => opt.MapFrom(s => s.COSISTEMA.TrimEnd()))
                .ForMember(d => d.NivelConsulta, opt => opt.MapFrom(s => s.INNIVELCONSULTA))
                .ForMember(d => d.NivelAtualiza, opt => opt.MapFrom(s => s.INNIVELATUALIZA))
                .ForMember(d => d.UltimaAlteracao, opt => opt.MapFrom(s => s.ULTALTERACAO.TrimEnd()))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.INSTATUS))
                .ForMember(d => d.CodigoUG, opt => opt.MapFrom(s => s.COUG))
                .ForMember(d => d.CodigoUO, opt => opt.MapFrom(s => s.COUO))
                .ForMember(d => d.DataValidade, opt => opt.MapFrom(s => s.DAVALIDADE))
                .ForMember(d => d.GestorUG, opt => opt.MapFrom(s => s.GESTORUG));

            CreateMap<UsuarioPerfil, UsuarioPerfilViewModel>()
                .ForMember(d => d.Cpf, opt => opt.MapFrom(s => s.NUUSUARIO.ToString().PadLeft(11, '0')))
                .ForMember(d => d.CodigoSistema, opt => opt.MapFrom(s => s.COSISTEMA.TrimEnd()))
                .ForMember(d => d.CodigoPerfil, opt => opt.MapFrom(s => s.COPERFIL.TrimEnd()))
                .ForMember(d => d.UltimaAlteracao, opt => opt.MapFrom(s => s.ULTALTERACAO));

            CreateMap<UsuarioFoto, UsuarioFotoViewModel>()
                .ForMember(d => d.Cpf, opt => opt.MapFrom(s => s.NUUSUARIO.ToString().PadLeft(11, '0')))
                .ForMember(d => d.NomeArquivo, opt => opt.MapFrom(s => s.NOMEARQUIVO.TrimEnd()))
                .ForMember(d => d.Foto, opt => opt.MapFrom(s => s.FOTO));

            CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(d => d.Cpf, opt => opt.MapFrom(s => s.NUUSUARIO.ToString().PadLeft(11, '0')))
                .ForMember(d => d.NomeUsuario, opt => opt.MapFrom(s => s.NOUSUARIO.TrimEnd()))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.NOEMAIL.Trim()))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.INSTATUS));

            CreateMap<Usuario, UsuarioDetailsViewModel>()
                .ForMember(d => d.Cpf, opt => opt.MapFrom(s => s.NUUSUARIO.ToString().PadLeft(11, '0')))
                .ForMember(d => d.NomeUsuario, opt => opt.MapFrom(s => s.NOUSUARIO.TrimEnd()))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.NOEMAIL.Trim()))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.INSTATUS));
        }
    }
}

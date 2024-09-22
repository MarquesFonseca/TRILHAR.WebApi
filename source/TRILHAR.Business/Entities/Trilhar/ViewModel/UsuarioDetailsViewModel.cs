using System.Collections.Generic;

namespace TRILHAR.Business.Entities.Trilhar.ViewModel
{
    public class UsuarioDetailsViewModel
    {
        public string Cpf { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public virtual List<UsuarioSistemaViewModel> UsuarioSistema { get; set; }
        public virtual List<UsuarioPerfilViewModel> UsuarioPerfil { get; set; }
        public virtual UsuarioFotoViewModel UsuarioFoto { get; set; }
    }
}

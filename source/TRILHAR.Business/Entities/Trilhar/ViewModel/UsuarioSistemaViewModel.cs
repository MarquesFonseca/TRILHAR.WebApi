using System;

namespace TRILHAR.Business.Entities.Trilhar.ViewModel
{
    public class UsuarioSistemaViewModel
    {
        public string Cpf { get; set; }
        public string CodigoSistema { get; set; }        
        public string NivelConsulta { get; set; }
        public string NivelAtualiza { get; set; }
        public string UltimaAlteracao { get; set; }
        public bool Status { get; set; }
        public int? CodigoUG { get; set; }
        public int? CodigoUO { get; set; }
        public DateTime? DataValidade { get; set; }
        public int? GestorUG { get; set; }
    }
}

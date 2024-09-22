using System;

namespace TRILHAR.Business.Entities.Trilhar
{
    public class UsuarioSistema
    {
        public string COSISTEMA { get; set; }
        public long NUUSUARIO { get; set; }
        public string INNIVELCONSULTA { get; set; }
        public string INNIVELATUALIZA { get; set; }
        public string ULTALTERACAO { get; set; }
        public int INSTATUS { get; set; }
        public int? COUG { get; set; }
        public int? COUO { get; set; }
        public DateTime? DAVALIDADE { get; set; }
        public int? GESTORUG { get; set; }
    }
}

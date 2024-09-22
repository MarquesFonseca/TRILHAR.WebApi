using System.Collections.Generic;

namespace TRILHAR.Business.Entities.Trilhar
{
    public class Usuario : EntityBase
    {
        public long NUUSUARIO { get; set; }
        public string NOUSUARIO { get; set; }
        public string NOEMAIL { get; set; }
        public int INSTATUS { get; set; }
    }
}

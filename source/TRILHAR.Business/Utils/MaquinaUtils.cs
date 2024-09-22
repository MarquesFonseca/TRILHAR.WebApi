using System;

namespace TRILHAR.Business.Utils
{
    public class MaquinaUtils
    {
        public static string RetornaEstacaoID()
        {
            return Environment.MachineName.Length > 9 ? Environment.MachineName.Substring(0, 9) : Environment.MachineName;
        }
    }
}

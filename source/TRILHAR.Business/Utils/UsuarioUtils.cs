namespace TRILHAR.Business.Utils
{
    public class UsuarioUtils
    {
        public static string FormataNomeCpfUsuario(string cpf, string nomeUsuario)
        {
            string cpfFormatado;
            cpfFormatado = cpf.Replace(".", "").Replace("-", "");
            cpfFormatado = cpfFormatado.Substring(3, 6);
            var inicio = "***";
            var final = "** ";

            if (string.IsNullOrEmpty(nomeUsuario))
                return inicio + cpfFormatado + final;
            else
                return inicio + cpfFormatado + final + "- " + nomeUsuario.Trim();
        }
    }
}
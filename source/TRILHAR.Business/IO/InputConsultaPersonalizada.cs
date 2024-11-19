
namespace TRILHAR.Business.IO
{
    public class InputConsultaPersonalizada
    {
        public string? ConsultaPersonalizada { get; set; } // Ex.: "SELECT CAMPO FROM TABELA"
        public string? Condicao { get; set; } // Ex.: "Ativo = @Ativo AND CodigoCadastro = @CodigoCadastro"
        public Dictionary<string, object?>? Parametros { get; set; } // Ex.: { "Ativo": true, "CodigoCadastro": "1484" }

        public InputConsultaPersonalizada()
        {
            ConsultaPersonalizada = "";
            Condicao = "";
            Parametros = new Dictionary<string, object?>();
        }
    }
}

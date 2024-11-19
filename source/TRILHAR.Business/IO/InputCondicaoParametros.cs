namespace TRILHAR.Business.IO
{
    public class InputCondicaoParametros
    {
        public string? Condicao { get; set; } // Ex.: "Ativo = @Ativo AND CodigoCadastro = @CodigoCadastro"
        public Dictionary<string, object?>? Parametros { get; set; } // Ex.: { "Ativo": true, "CodigoCadastro": "1484" }

        public InputCondicaoParametros()
        {
            Condicao = "";
            Parametros = new Dictionary<string, object?>();
        }
    }
}

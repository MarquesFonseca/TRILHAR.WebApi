namespace TRILHAR.Business.IO
{
    public class InputPaginado
    {
        public string? Condicao { get; set; } // Ex.: "Ativo = @Ativo AND CodigoCadastro = @CodigoCadastro"
        public Dictionary<string, object?>? Parametros { get; set; } // Ex.: { "Ativo": true, "CodigoCadastro": "1484" }
        public bool IsPaginacao { get; set; } // Ex.: true
        public int Page { get; set; }  //Página atual (Ex.: 1)
        public int PageSize { get; set; } // Tamanho da página (Ex.: 10)

        public InputPaginado()
        {
            Condicao = "";
            Parametros = new Dictionary<string, object?>();
            IsPaginacao = false;
            Page = 1;
            PageSize = 10;
        }
    }
}

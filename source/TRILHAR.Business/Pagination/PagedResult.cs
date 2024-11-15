namespace TRILHAR.Business.Pagination;

public class PagedResult<T> where T : class
{
    public int PaginaAtual { get; set; }
    public int TotalPaginas { get; set; }
    public int TamanhoPagina { get; set; }
    public int TotalItens { get; set; }
    public int PrimeiroItemNaPagina => TotalPaginas == 1 && TotalItens == 1 ? 1 : (PaginaAtual - 1) * TamanhoPagina + 1;
    public int UltimoItemNaPagina => TotalPaginas == 1 && TotalItens == 1 ? 1 : Math.Min(PaginaAtual * TamanhoPagina, TotalItens);
    public List<T> Dados { get; set; }

    public PagedResult<TN> CopyToDtoMapping<TN>(List<TN> newResult) where TN : class
    {
        return new PagedResult<TN>
        {
            PaginaAtual = this.PaginaAtual,
            TotalPaginas = this.TotalPaginas,
            TamanhoPagina = this.TamanhoPagina,
            TotalItens = this.TotalItens,
            Dados = newResult
        };
    }

    public PagedResult()
    {
        Dados = new List<T>();
        PaginaAtual = 0;
        TotalPaginas = 0;
        TamanhoPagina = 0;
        TotalItens = 0;
    }
}

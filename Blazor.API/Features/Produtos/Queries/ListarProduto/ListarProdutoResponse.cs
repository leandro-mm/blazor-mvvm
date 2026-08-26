namespace Blazor.API.Features.Produtos.Queries.ListarProduto;

public class ListarProdutoResponse
{
    public List<ProdutoDto> Produtos { get; set; } = new();
    public int TotalRegistros { get; set; }
    public int PaginaAtual { get; set; } = 1;
    public int TotalPaginas { get; set; }
    public int TamanhoPagina { get; set; } = 10;
}
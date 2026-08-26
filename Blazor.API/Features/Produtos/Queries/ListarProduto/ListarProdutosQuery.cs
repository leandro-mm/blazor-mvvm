using Blazor.API.Features.Produtos.Queries.ListarProduto;
using MediatR;

namespace Blazor.API.Features.Produtos.Queries.ListarProdutos;

public class ListarProdutosQuery : IRequest<ListarProdutoResponse>
{
    public string? FiltroNome { get; set; }
    public string? Categoria { get; set; }
    public int Pagina { get; set; } = 1;
    public int TamanhoPagina { get; set; } = 10;
}
using MediatR;

namespace Blazor.API.Features.Produtos.Commands.CriarProduto;

public class CriarProdutoCommand : IRequest<CriarProdutoResponse>
{
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int QuantidadeEstoque { get; set; }
    public string Categoria { get; set; } = string.Empty;
}
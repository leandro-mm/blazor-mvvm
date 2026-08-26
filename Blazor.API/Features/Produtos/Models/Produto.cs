namespace Blazor.API.Features.Produtos.Models;

public class Produto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int QuantidadeEstoque { get; set; }
    public string Categoria { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
}
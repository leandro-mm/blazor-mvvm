namespace Blazor.API.Features.Produtos.Commands.DeletarProduto;

public class DeletarProdutoResponse
{
    public Guid Id { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public bool Sucesso { get; set; }
}
namespace Blazor.API.Features.Produtos.Commands.Editar;

public class EditarProdutoResponse
{
    public Guid Id { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public bool Sucesso { get; set; }
}
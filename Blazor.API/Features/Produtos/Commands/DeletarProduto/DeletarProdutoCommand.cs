using MediatR;

namespace Blazor.API.Features.Produtos.Commands.DeletarProduto;

public class DeletarProdutoCommand : IRequest<DeletarProdutoResponse>
{
    public Guid Id { get; set; }
}
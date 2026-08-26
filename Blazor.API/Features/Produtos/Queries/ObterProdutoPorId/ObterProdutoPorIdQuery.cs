using MediatR;

namespace Blazor.API.Features.Produtos.Queries.ObterProdutoPorId;

public class ObterProdutoPorIdQuery : IRequest<ObterProdutoPorIdResponse>
{
    public Guid Id { get; set; }
}
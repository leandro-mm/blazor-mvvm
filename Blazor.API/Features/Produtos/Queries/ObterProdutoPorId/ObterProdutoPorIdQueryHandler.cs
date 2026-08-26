using Blazor.API.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blazor.API.Features.Produtos.Queries.ObterProdutoPorId;

public class ObterProdutoPorIdQueryHandler : IRequestHandler<ObterProdutoPorIdQuery, ObterProdutoPorIdResponse>
{
    private readonly ApplicationDbContext _context;

    public ObterProdutoPorIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ObterProdutoPorIdResponse> Handle(ObterProdutoPorIdQuery request, CancellationToken cancellationToken)
    {
        var produto = await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (produto == null)
        {
            throw new KeyNotFoundException($"Produto com ID {request.Id} não encontrado");
        }

        return new ObterProdutoPorIdResponse
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Preco = produto.Preco,
            QuantidadeEstoque = produto.QuantidadeEstoque,
            Categoria = produto.Categoria,
            DataCriacao = produto.DataCriacao
        };
    }
}
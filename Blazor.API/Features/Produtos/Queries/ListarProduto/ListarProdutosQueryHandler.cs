using Blazor.API.Features.Produtos.Queries.ListarProduto;
using Blazor.API.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blazor.API.Features.Produtos.Queries.ListarProdutos;

public class ListarProdutosQueryHandler :
    IRequestHandler<ListarProdutosQuery, ListarProdutoResponse>
{
    private readonly ApplicationDbContext _context;

    public ListarProdutosQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ListarProdutoResponse> Handle(ListarProdutosQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Produtos.AsQueryable();

        if (!string.IsNullOrEmpty(request.FiltroNome))
            query = query.Where(p => p.Nome.Contains(request.FiltroNome));

        if (!string.IsNullOrEmpty(request.Categoria))
            query = query.Where(p => p.Categoria == request.Categoria);

        var total = await query.CountAsync(cancellationToken);

        var produtos = await query
           .Skip((request.Pagina - 1) * request.TamanhoPagina)
           .Take(request.TamanhoPagina)
           .Select(p => new ProdutoDto
           {
               Id = p.Id,
               Nome = p.Nome,
               Preco = p.Preco,
               Categoria = p.Categoria,
               QuantidadeEstoque = p.QuantidadeEstoque
           })
           .ToListAsync(cancellationToken);

        return new ListarProdutoResponse
        {
            Produtos = produtos,
            TotalRegistros = total,
            PaginaAtual = request.Pagina,
            TotalPaginas = (int)Math.Ceiling(total / (double)request.TamanhoPagina)
        };
    }
}
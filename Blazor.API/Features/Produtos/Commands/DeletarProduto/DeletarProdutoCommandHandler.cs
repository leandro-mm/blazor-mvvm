using Blazor.API.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blazor.API.Features.Produtos.Commands.DeletarProduto;

public class DeletarProdutoCommandHandler : IRequestHandler<DeletarProdutoCommand, DeletarProdutoResponse>
{
    private readonly ApplicationDbContext _context;

    public DeletarProdutoCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DeletarProdutoResponse> Handle(DeletarProdutoCommand request, CancellationToken cancellationToken)
    {
        // Buscar o produto existente
        var produto = await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (produto == null)
        {
            return new DeletarProdutoResponse
            {
                Id = request.Id,
                Sucesso = false,
                Mensagem = "Produto não encontrado"
            };
        }

        // Remover o produto
        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeletarProdutoResponse
        {
            Id = produto.Id,
            Sucesso = true,
            Mensagem = $"Produto '{produto.Nome}' deletado com sucesso!"
        };
    }
}
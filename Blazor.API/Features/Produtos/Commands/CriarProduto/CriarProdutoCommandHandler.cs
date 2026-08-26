using Blazor.API.Features.Produtos.Models;
using Blazor.API.Infrastructure.Data;
using MediatR;

namespace Blazor.API.Features.Produtos.Commands.CriarProduto;

public class CriarProdutoCommandHandler : IRequestHandler<CriarProdutoCommand, CriarProdutoResponse>
{
    private readonly ApplicationDbContext _context;
    public CriarProdutoCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<CriarProdutoResponse> Handle(CriarProdutoCommand request, CancellationToken cancellationToken)
    {
        // Validação de negócio
        if (request.Preco < 0)
            throw new InvalidOperationException("Preço não pode ser negativo");

        var produto = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            Preco = request.Preco,
            QuantidadeEstoque = request.QuantidadeEstoque,
            Categoria = request.Categoria,
            DataCriacao = DateTime.Now
        };

        await _context.Produtos.AddAsync(produto, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new CriarProdutoResponse
        {
            Id = produto.Id,
            Mensagem = "Produto criado com sucesso!"
        };
    }
}
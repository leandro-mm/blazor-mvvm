using Blazor.API.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blazor.API.Features.Produtos.Commands.Editar;

public class EditarProdutoCommandHandler : IRequestHandler<EditarProdutoCommand, EditarProdutoResponse>
{
    private readonly ApplicationDbContext _context;

    public EditarProdutoCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<EditarProdutoResponse> Handle(EditarProdutoCommand request, CancellationToken cancellationToken)
    {
        // Buscar o produto existente
        var produto = await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (produto == null)
        {
            return new EditarProdutoResponse
            {
                Id = request.Id,
                Sucesso = false,
                Mensagem = "Produto não encontrado"
            };
        }

        // Validar regras de negócio
        if (request.Preco < 0)
        {
            return new EditarProdutoResponse
            {
                Id = request.Id,
                Sucesso = false,
                Mensagem = "Preço não pode ser negativo"
            };
        }

        // Atualizar os dados
        produto.Nome = request.Nome;
        produto.Preco = request.Preco;
        produto.QuantidadeEstoque = request.QuantidadeEstoque;
        produto.Categoria = request.Categoria;

        // Salvar alterações
        await _context.SaveChangesAsync(cancellationToken);

        return new EditarProdutoResponse
        {
            Id = produto.Id,
            Sucesso = true,
            Mensagem = "Produto atualizado com sucesso!"
        };

    }
}
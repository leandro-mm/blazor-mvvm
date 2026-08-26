using Blazor.API.Features.Produtos.Models;
using Blazor.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Tests.helpers;

public static class TestDbContextFactory
{
    public static ApplicationDbContext CreateInMemoryDbContext(string databaseName = null!)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName ?? Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    public static ApplicationDbContext CreateInMemoryDbContextWithSeedData()
    {
        var context = CreateInMemoryDbContext();

        // Adicionar dados de teste
        context.Produtos.AddRange(new List<Produto>
        {
            new()
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Nome = "Produto Teste 1",
                Preco = 100.00m,
                QuantidadeEstoque = 10,
                Categoria = "Teste",
                DataCriacao = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Nome = "Produto Teste 2",
                Preco = 200.00m,
                QuantidadeEstoque = 20,
                Categoria = "Teste",
                DataCriacao = DateTime.UtcNow
            }
        });

        context.SaveChanges();
        return context;
    }
}
using Blazor.API.Features.Produtos.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.API.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
    }
    public DbSet<Produto> Produtos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Categoria).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Preco).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(p => p.DataCriacao).HasDefaultValueSql("CURRENT_TIMESTAMP");

            SeedProdutoData(modelBuilder);
        });
    }

    private void SeedProdutoData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>().HasData(
            new Produto
            {
                Id = Guid.NewGuid(),
                Nome = "Produto 1",
                Categoria = "Categoria A",
                Preco = 10.99m,
                QuantidadeEstoque = 100,
                DataCriacao = DateTime.UtcNow
            },
            new Produto
            {
                Id = Guid.NewGuid(),
                Nome = "Produto 2",
                Categoria = "Categoria B",
                Preco = 20.50m,
                QuantidadeEstoque = 50,
                DataCriacao = DateTime.UtcNow
            },
            new Produto
            {
                Id = Guid.NewGuid(),
                Nome = "Produto 3",
                Categoria = "Categoria A",
                Preco = 15.75m,
                QuantidadeEstoque = 75,
                DataCriacao = DateTime.UtcNow
            }
        );
    }
}
using Blazor.API.Features.Produtos.Commands.CriarProduto;
using Blazor.API.Features.Produtos.Models;

namespace Blazor.Tests.Fixtures;

public static class TestDataFixture
{
    public static CriarProdutoCommand CriarCommandValido()
    {
        return new CriarProdutoCommand
        {
            Nome = "Notebook Dell XPS",
            Preco = 3500.00m,
            QuantidadeEstoque = 15,
            Categoria = "Eletrônicos"
        };
    }

    public static CriarProdutoCommand CriarCommandComNomeVazio()
    {
        return new CriarProdutoCommand
        {
            Nome = "",
            Preco = 3500.00m,
            QuantidadeEstoque = 15,
            Categoria = "Eletrônicos"
        };
    }

    public static CriarProdutoCommand CriarCommandComPrecoNegativo()
    {
        return new CriarProdutoCommand
        {
            Nome = "Notebook Dell XPS",
            Preco = -100.00m,
            QuantidadeEstoque = 15,
            Categoria = "Eletrônicos"
        };
    }

    public static CriarProdutoCommand CriarCommandComCategoriaVazia()
    {
        return new CriarProdutoCommand
        {
            Nome = "Notebook Dell XPS",
            Preco = 3500.00m,
            QuantidadeEstoque = 15,
            Categoria = ""
        };
    }

    public static CriarProdutoCommand CriarCommandComEstoqueNegativo()
    {
        return new CriarProdutoCommand
        {
            Nome = "Notebook Dell XPS",
            Preco = 3500.00m,
            QuantidadeEstoque = -5,
            Categoria = "Eletrônicos"
        };
    }

    public static CriarProdutoCommand CriarCommandComNomeMuitoLongo()
    {
        return new CriarProdutoCommand
        {
            Nome = new string('A', 101), // 101 caracteres
            Preco = 3500.00m,
            QuantidadeEstoque = 15,
            Categoria = "Eletrônicos"
        };
    }

    public static Produto CriarProdutoModel()
    {
        return new Produto
        {
            Id = Guid.NewGuid(),
            Nome = "Produto Modelo",
            Preco = 150.00m,
            QuantidadeEstoque = 30,
            Categoria = "Modelo",
            DataCriacao = DateTime.UtcNow
        };
    }
}
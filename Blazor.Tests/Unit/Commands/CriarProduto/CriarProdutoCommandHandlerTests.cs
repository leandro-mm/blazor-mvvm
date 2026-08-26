using Blazor.API.Features.Produtos.Commands.CriarProduto;
using Blazor.API.Infrastructure.Data;
using Blazor.Tests.Fixtures;
using Blazor.Tests.helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Tests.Unit.Commands.CriarProduto;

public class CriarProdutoCommandHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly CriarProdutoCommandHandler _handler;

    public CriarProdutoCommandHandlerTests()
    {
        _context = TestDbContextFactory.CreateInMemoryDbContext();
        _handler = new CriarProdutoCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_DeveCriarProduto_QuandoCommandValido()
    {
        // Arrange
        var command = TestDataFixture.CriarCommandValido();
        var initialCount = await _context.Produtos.CountAsync();

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().NotBeEmpty();
        response.Mensagem.Should().Be("Produto criado com sucesso!");

        var produtoCriado = await _context.Produtos.FindAsync(response.Id);
        produtoCriado.Should().NotBeNull();
        produtoCriado!.Nome.Should().Be(command.Nome);
        produtoCriado.Preco.Should().Be(command.Preco);
        produtoCriado.QuantidadeEstoque.Should().Be(command.QuantidadeEstoque);
        produtoCriado.Categoria.Should().Be(command.Categoria);

        var finalCount = await _context.Produtos.CountAsync();
        finalCount.Should().Be(initialCount + 1);
    }

    [Fact]
    public async Task Handle_DeveLancarExcecao_QuandoPrecoNegativo()
    {
        // Arrange
        var command = new CriarProdutoCommand
        {
            Nome = "Produto Teste",
            Preco = -100.00m,
            QuantidadeEstoque = 10,
            Categoria = "Teste"
        };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _handler.Handle(command, CancellationToken.None)
        );
    }

    [Fact]
    public async Task Handle_DeveCriarProdutoComDataCriacao_QuandoCommandValido()
    {
        // Arrange
        var command = TestDataFixture.CriarCommandValido();
        var antesCriacao = DateTime.Now;

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        var produto = await _context.Produtos.FindAsync(response.Id);
        produto.Should().NotBeNull();
        produto!.DataCriacao.Should().BeOnOrAfter(antesCriacao);
        produto.DataCriacao.Should().BeOnOrBefore(DateTime.Now);
    }

    [Fact]
    public async Task Handle_DeveGerarIdUnico_QuandoCriarMultiplosProdutos()
    {
        // Arrange
        var command1 = new CriarProdutoCommand
        {
            Nome = "Produto 1",
            Preco = 100,
            QuantidadeEstoque = 10,
            Categoria = "Teste"
        };

        var command2 = new CriarProdutoCommand
        {
            Nome = "Produto 2",
            Preco = 200,
            QuantidadeEstoque = 20,
            Categoria = "Teste"
        };

        // Act
        var response1 = await _handler.Handle(command1, CancellationToken.None);
        var response2 = await _handler.Handle(command2, CancellationToken.None);

        // Assert
        response1.Id.Should().NotBe(response2.Id);
    }

    [Fact]
    public async Task Handle_DevePersistirTodosOsCampos_QuandoCommandValido()
    {
        // Arrange
        var command = new CriarProdutoCommand
        {
            Nome = "Produto Completo",
            Preco = 999.99m,
            QuantidadeEstoque = 42,
            Categoria = "Premium"
        };

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        var produto = await _context.Produtos.FindAsync(response.Id);
        produto.Should().NotBeNull();
        produto!.Nome.Should().Be("Produto Completo");
        produto.Preco.Should().Be(999.99m);
        produto.QuantidadeEstoque.Should().Be(42);
        produto.Categoria.Should().Be("Premium");
    }
}
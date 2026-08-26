using Blazor.API.Features.Produtos.Commands.CriarProduto;
using Blazor.API.Features.Produtos.Validators;
using Blazor.Tests.Fixtures;
using FluentValidation.TestHelper;

namespace Blazor.Tests.Unit.Commands.CriarProduto;

public class CriarProdutoValidatorTests
{
    private readonly CriarProdutoValidator _validator;

    public CriarProdutoValidatorTests()
    {
        _validator = new CriarProdutoValidator();
    }

    [Fact]
    public void Validator_DevePassar_QuandoCommandValido()
    {
        // Arrange
        var command = TestDataFixture.CriarCommandValido();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_DeveFalhar_QuandoNomeVazio()
    {
        // Arrange
        var command = TestDataFixture.CriarCommandComNomeVazio();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Nome)
            .WithErrorMessage("Nome é obrigatório");
    }

    [Fact]
    public void Validator_DeveFalhar_QuandoPrecoNegativo()
    {
        // Arrange
        var command = TestDataFixture.CriarCommandComPrecoNegativo();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Preco)
            .WithErrorMessage("Preço deve ser maior que zero");
    }

    [Fact]
    public void Validator_DeveFalhar_QuandoCategoriaVazia()
    {
        // Arrange
        var command = TestDataFixture.CriarCommandComCategoriaVazia();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Categoria)
            .WithErrorMessage("Categoria é obrigatória");
    }

    [Fact]
    public void Validator_DeveFalhar_QuandoEstoqueNegativo()
    {
        // Arrange
        var command = TestDataFixture.CriarCommandComEstoqueNegativo();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.QuantidadeEstoque)
            .WithErrorMessage("Quantidade em estoque não pode ser negativa");
    }

    [Fact]
    public void Validator_DeveFalhar_QuandoNomeMuitoLongo()
    {
        // Arrange
        var command = TestDataFixture.CriarCommandComNomeMuitoLongo();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Nome)
            .WithErrorMessage("Nome deve ter no máximo 100 caracteres");
    }

    [Theory]
    [InlineData("Produto A", 10.50, 5, "Categoria A")]
    [InlineData("Produto B", 999.99, 0, "Categoria B")]
    [InlineData("X", 1.00, 100, "C")]
    public void Validator_DevePassar_ComDiversosValoresValidos(
        string nome, decimal preco, int quantidade, string categoria)
    {
        // Arrange
        var command = new CriarProdutoCommand
        {
            Nome = nome,
            Preco = preco,
            QuantidadeEstoque = quantidade,
            Categoria = categoria
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
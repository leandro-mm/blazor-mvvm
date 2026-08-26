using Blazor.API.Features.Produtos.Commands.CriarProduto;
using Blazor.API.Features.Produtos.Queries.ListarProduto;
using Blazor.API.Features.Produtos.Queries.ListarProdutos;
using Blazor.API.Features.Produtos.viewModels;
using Blazor.Tests.Fixtures;
using FluentAssertions;
using MediatR;
using Moq;

namespace Blazor.Tests.Unit.Commands.CriarProduto.ViewModels;

public class ProdutoViewModelTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ProdutoViewModel _viewModel;

    public ProdutoViewModelTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _viewModel = new ProdutoViewModel(_mediatorMock.Object);
    }

    [Fact]
    public async Task CriarProdutoAsync_DeveChamarMediator_QuandoCommandValido()
    {
        // Arrange
        var command = TestDataFixture.CriarCommandValido();
        var expectedResponse = new CriarProdutoResponse
        {
            Id = Guid.NewGuid(),
            Mensagem = "Produto criado com sucesso!"
        };

        // Setup para CriarProdutoCommand
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CriarProdutoCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Setup para ListarProdutosQuery com retorno válido
        var listarResponse = new ListarProdutoResponse
        {
            Produtos = new List<ProdutoDto>() // Importante: inicializar a lista
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<ListarProdutosQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(listarResponse);

        // Act
        var response = await _viewModel.CriarProdutoAsync(command);

        // Assert
        response.Should().Be(expectedResponse);
        _mediatorMock.Verify(m => m.Send(It.IsAny<CriarProdutoCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        _mediatorMock.Verify(m => m.Send(It.IsAny<ListarProdutosQuery>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }

    [Fact]
    public async Task CriarProdutoAsync_DeveDefinirIsLoading_QuandoEmExecucao()
    {
        // Arrange
        var command = TestDataFixture.CriarCommandValido();
        var tcs = new TaskCompletionSource<CriarProdutoResponse>();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CriarProdutoCommand>(), It.IsAny<CancellationToken>()))
            .Returns(tcs.Task);

        // Act
        var task = _viewModel.CriarProdutoAsync(command);

        // Assert - durante execução
        _viewModel.IsLoading.Should().BeTrue();

        // Complete a task para liberar
        tcs.SetResult(new CriarProdutoResponse());
        await task;

        // Assert - após execução
        _viewModel.IsLoading.Should().BeFalse();
    }

    [Fact]
    public async Task CriarProdutoAsync_DeveDefinirErrorMessage_QuandoLancaExcecao()
    {
        // Arrange
        var command = TestDataFixture.CriarCommandValido();
        var exceptionMessage = "Erro ao criar produto";

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CriarProdutoCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception(exceptionMessage));

        // Act
        Func<Task> act = async () => await _viewModel.CriarProdutoAsync(command);
        await act.Should().ThrowAsync<Exception>(); // Usar FluentAssertions

        // Assert
        _viewModel.ErrorMessage.Should().Be(exceptionMessage);
        _viewModel.IsLoading.Should().BeFalse();
    }

    [Fact]
    public async Task CriarProdutoAsync_DeveLimparErrorMessage_QuandoComandoValido()
    {
        // Arrange
        var command = TestDataFixture.CriarCommandValido();
        _viewModel.ErrorMessage = "Erro anterior";

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CriarProdutoCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CriarProdutoResponse());

        var listarResponse = new ListarProdutoResponse
        {
            Produtos = new List<ProdutoDto>()
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<ListarProdutosQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(listarResponse);

        // Act
        await _viewModel.CriarProdutoAsync(command);

        // Assert
        _viewModel.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public async Task CriarProdutoAsync_DeveChamarListarProdutos_QuandoCriacaoBemSucedida()
    {
        // Arrange
        var command = TestDataFixture.CriarCommandValido();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CriarProdutoCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CriarProdutoResponse());

        var listarResponse = new ListarProdutoResponse
        {
            Produtos = new List<ProdutoDto>()
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<ListarProdutosQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(listarResponse);

        // Act
        await _viewModel.CriarProdutoAsync(command);

        // Assert
        _mediatorMock.Verify(
            m => m.Send(It.IsAny<ListarProdutosQuery>(), It.IsAny<CancellationToken>()),
            Times.AtLeastOnce
        );
    }
}
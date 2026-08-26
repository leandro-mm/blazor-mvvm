using Blazor.API.Features.Produtos.Commands.CriarProduto;
using Blazor.API.Features.Produtos.Commands.DeletarProduto;
using Blazor.API.Features.Produtos.Queries.ListarProduto;
using Blazor.API.Features.Produtos.Queries.ListarProdutos;
using MediatR;

namespace Blazor.API.Features.Produtos.viewModels;

public class ProdutoViewModel
{
    private readonly IMediator _mediator;
    public ListarProdutosResponse? Produtos { get; set; }
    public bool IsLoading { get; set; }
    public string? ErrorMessage { get; set; }
    public string? SuccessMessage { get; set; }
    public ProdutoViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task CarregarProdutosAsync(ListarProdutosQuery query)
    {
        try
        {
            IsLoading = true;
            Produtos = await _mediator.Send(query);
            ErrorMessage = null;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task<CriarProdutoResponse> CriarProdutoAsync(CriarProdutoCommand command)
    {
        return await _mediator.Send(command);
    }
    // public async Task<EditarProdutoResponse> EditarProdutoAsync(EditarProdutoCommand command)
    // {
    //     try
    //     {
    //         IsLoading = true;
    //         ErrorMessage = null;

    //         var response = await _mediator.Send(command);

    //         // Recarregar a lista após editar
    //         if (response.Sucesso)
    //         {
    //             await CarregarProdutosAsync(new ListarProdutosQuery());
    //         }

    //         return response;
    //     }
    //     catch (Exception ex)
    //     {
    //         ErrorMessage = ex.Message;
    //         throw;
    //     }
    //     finally
    //     {
    //         IsLoading = false;
    //     }
    // }
    public async Task<DeletarProdutoResponse> DeletarProdutoAsync(Guid id)
    {
        try
        {
            IsLoading = true;
            ErrorMessage = null;
            SuccessMessage = null;

            var command = new DeletarProdutoCommand { Id = id };
            var response = await _mediator.Send(command);

            // Recarregar a lista após deletar
            if (response.Sucesso)
            {
                await CarregarProdutosAsync(new ListarProdutosQuery());
                SuccessMessage = response.Mensagem;
            }
            else
            {
                ErrorMessage = response.Mensagem;
            }

            return response;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return new DeletarProdutoResponse
            {
                Id = id,
                Sucesso = false,
                Mensagem = ex.Message
            };
        }
        finally
        {
            IsLoading = false;
        }
    }
}
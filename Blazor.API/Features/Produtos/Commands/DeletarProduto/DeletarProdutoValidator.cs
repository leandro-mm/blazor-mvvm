using FluentValidation;

namespace Blazor.API.Features.Produtos.Commands.DeletarProduto;

public class DeletarProdutoValidator : AbstractValidator<DeletarProdutoCommand>
{
    public DeletarProdutoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID do produto é obrigatório");
    }
}
using Blazor.API.Features.Produtos.Commands.CriarProduto;
using FluentValidation;

namespace Blazor.API.Features.Produtos.Validators;

public class CriarProdutoValidator : AbstractValidator<CriarProdutoCommand>
{
    public CriarProdutoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

        RuleFor(x => x.Preco)
            .GreaterThan(0).WithMessage("Preço deve ser maior que zero");

        RuleFor(x => x.QuantidadeEstoque)
            .GreaterThanOrEqualTo(0).WithMessage("Quantidade em estoque não pode ser negativa");

        RuleFor(x => x.Categoria)
            .NotEmpty().WithMessage("Categoria é obrigatória")
            .MaximumLength(50).WithMessage("Categoria deve ter no máximo 50 caracteres");
    }
}
using Blazor.API.Features.Produtos.Models;

namespace Blazor.API.Data.Repositories;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> GetAllAsync();
    Task<Produto?> GetByIdAsync(Guid id);
    Task AddAsync(Produto produto);
    Task UpdateAsync(Produto produto);
    Task DeleteAsync(Guid id);
}
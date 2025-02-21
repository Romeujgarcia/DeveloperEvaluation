// IProductRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository
    {
        
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync(int page, int size, string order, string? category = null, decimal? minPrice = null, decimal? maxPrice = null, string? title = null, string? titleWildcard = null);
        Task<int> GetTotalProductsCountAsync(); // Novo método para contar produtos
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
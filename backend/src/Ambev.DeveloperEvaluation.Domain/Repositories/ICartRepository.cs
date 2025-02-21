// ICartRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByIdAsync(int id);
        Task<List<Cart>> GetAllCartsAsync(int page, int size, string order);
        Task AddCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(int id);
        
        
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DefaultContext _context; // Substitua YourDbContext pelo seu contexto de banco de dados

        public CartRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            return await _context.Carts.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Cart>> GetAllCartsAsync(int page, int size, string order)
        {
            var query = _context.Carts.Include(c => c.Products).AsQueryable();

            // Aplicar ordenação se necessário
            if (!string.IsNullOrEmpty(order))
            {
                query = query.OrderBy(c => c.Id); // Ajuste conforme necessário
            }

            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task AddCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(int id)
        {
            var cart = await GetCartByIdAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }
    }
}
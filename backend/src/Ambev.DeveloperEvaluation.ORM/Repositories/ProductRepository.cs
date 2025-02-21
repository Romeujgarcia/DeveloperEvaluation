using System.Collections.Generic;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DefaultContext _context; // Substitua YourDbContext pelo seu contexto de banco de dados

        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalProductsCountAsync()
        {
            return await _context.Products.CountAsync(); // Supondo que você tenha um DbSet<Product> chamado Products
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
              .Include(p => p.Rating) // Certifique-se de incluir a propriedade Rating se ela for uma entidade relacionada
              .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(int page, int size, string order, string? category = null, decimal? minPrice = null, decimal? maxPrice = null, string? title = null, string? titleWildcard = null)
        {
            var products = _context.Products.Include(p => p.Rating).AsQueryable();

            // Aplicar filtros
            if (!string.IsNullOrEmpty(category))
            {
                if (category.StartsWith("*"))
                {
                    var trimmedCategory = category.TrimStart('*');
                    products = products.Where(p => p.Category.EndsWith(trimmedCategory));
                }
                else if (category.EndsWith("*"))
                {
                    var trimmedCategory = category.TrimEnd('*');
                    products = products.Where(p => p.Category.StartsWith(trimmedCategory));
                }
                else
                {
                    products = products.Where(p => p.Category == category);
                }
            }
            // Aplicar filtro de preço mínimo e máximo

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }

            if (!string.IsNullOrEmpty(title))
            {
                products = products.Where(p => p.Title.Contains(title));
            }

            // Suporte a caracteres curinga para o título
            if (!string.IsNullOrEmpty(titleWildcard))
            {
                if (titleWildcard.StartsWith("*"))
                {
                    titleWildcard = titleWildcard.TrimStart('*');
                    products = products.Where(p => p.Title.EndsWith(titleWildcard));
                }
                else if (titleWildcard.EndsWith("*"))
                {
                    titleWildcard = titleWildcard.TrimEnd('*');
                    products = products.Where(p => p.Title.StartsWith(titleWildcard));
                }
                else
                {
                    products = products.Where(p => p.Title.Contains(titleWildcard));
                }
            }

            // Aplicar ordenação
            if (!string.IsNullOrEmpty(order))
            {
                var orderParams = order.Split(',');
                foreach (var orderParam in orderParams)
                {
                    var trimmedParam = orderParam.Trim();
                    if (trimmedParam.EndsWith(" desc"))
                    {
                        var property = trimmedParam.Substring(0, trimmedParam.Length - 5).Trim();
                        products = products.OrderByDescending(p => EF.Property<object>(p, property));
                    }
                    else
                    {
                        var property = trimmedParam.Trim();
                        products = products.OrderBy(p => EF.Property<object>(p, property));
                    }
                }
            }

            return await products
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }
        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

    }
}
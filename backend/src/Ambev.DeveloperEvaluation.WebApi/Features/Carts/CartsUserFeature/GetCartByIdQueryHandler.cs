using Ambev.DeveloperEvaluation.Application.Carts.Queries;
using Ambev.DeveloperEvaluation.Application.Models;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.Handlers
{
    public class GetCartByIdQueryHandler : IRequestHandler<GetCartByIdQuery, CartDto>
    {
        private readonly DefaultContext _context;

        public GetCartByIdQueryHandler(DefaultContext context)
        {
            _context = context;
        }
        public async Task<CartDto> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _context.Carts
                .Include(c => c.Products) // Inclui os produtos do carrinho
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (cart == null)
            {
                return null; // Ou lance uma exceção, dependendo da sua lógica
            }

            return new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Date = cart.Date, // Formato ISO 8601
                Products = cart.Products?.Select(p => new CartProductDto
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList() ?? new List<CartProductDto>() // Mapeia os produtos para o DTO
            };
        }
    }
}
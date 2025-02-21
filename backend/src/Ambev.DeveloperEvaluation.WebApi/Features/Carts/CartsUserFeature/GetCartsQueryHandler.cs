using Ambev.DeveloperEvaluation.Application.Carts.Queries;
using Ambev.DeveloperEvaluation.Application.Models;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.Handlers
{
   public class GetCartsQueryHandler : IRequestHandler<GetCartsQuery, List<CartDto>>
{
    private readonly ICartRepository _cartRepository;

    public GetCartsQueryHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<List<CartDto>> Handle(GetCartsQuery request, CancellationToken cancellationToken)
    {
        var carts = await _cartRepository.GetAllCartsAsync(request.Page, request.Size, request.Order);
        
        // Aqui você deve mapear Cart para CartDto
        return carts.Select(cart => new CartDto
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Date = cart.Date,
            Products = cart.Products != null ? cart.Products.Select(p => new CartProductDto
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity
            }).ToList() : new List<CartProductDto>()
        }).ToList();
    }
}
}

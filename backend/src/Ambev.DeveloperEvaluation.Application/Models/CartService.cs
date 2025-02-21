using Ambev.DeveloperEvaluation.Application.Models;
using Ambev.DeveloperEvaluation.Domain.Entities;

public class CartService
{
    public CartDto ConvertToCartDto(Cart cart)
    {
        var cartProductsDto = cart.Products.Select(cp => new CartProductDto
        {
            ProductId = cp.ProductId,
            Quantity = cp.Quantity
        }).ToList();

        return new CartDto
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Date = cart.Date,
            Products = cartProductsDto
        };
    }
}
using MediatR;
using Ambev.DeveloperEvaluation.Application.Models; // Certifique-se de que você tem a referência correta
using Ambev.DeveloperEvaluation.Domain.Repositories; // Certifique-se de que você tem a referência correta
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Carts.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.Handlers
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, CartDto> // Altere para CartDto
    {
        private readonly ICartRepository _cartRepository;

        public CreateCartHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartDto> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            // Lógica para criar o carrinho
            var cart = new Cart
            {
                UserId = request.UserId,
                Date = request.Date,
                Products = request.Products // Certifique-se de que este é o tipo correto
            };

            // Salve o carrinho no repositório
            await _cartRepository.AddCartAsync(cart);

            // Mapeie o carrinho para CartDto
            return new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Date = cart.Date,
                Products = cart.Products.Select(p => new CartProductDto
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList()
            };
        }
    }
}
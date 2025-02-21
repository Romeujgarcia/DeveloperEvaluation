using Ambev.DeveloperEvaluation.Application.Carts.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, Unit>
{
    private readonly ICartRepository _cartRepository;

    public UpdateCartCommandHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<Unit> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCartByIdAsync(request.Id);
        if (cart == null)
        {
            throw new NotFoundException("Cart not found");
        }

        // Atualizar o carrinho com os novos produtos
        cart.UserId = request.UserId; // Atualiza o UserId se necessário
        cart.Date = request.Date; // Atualiza a data se necessário
        cart.Products = request.Products.Select(p => new CartProduct
        {
            ProductId = p.ProductId,
            Quantity = p.Quantity,
            CartId = cart.Id // Certifique-se de que CartId está correto
        }).ToList();

        await _cartRepository.UpdateCartAsync(cart); // Salva as alterações

        return Unit.Value; // Retorna Unit.Value para indicar sucesso
    }
}
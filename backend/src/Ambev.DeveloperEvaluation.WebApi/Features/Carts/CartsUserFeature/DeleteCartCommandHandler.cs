using Ambev.DeveloperEvaluation.Application.Carts.Commands;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, Unit>
{
    private readonly ICartRepository _cartRepository;

    public DeleteCartCommandHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<Unit> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCartByIdAsync(request.Id);
        if (cart == null)
        {
            throw new NotFoundException("Cart not found");
        }

        await _cartRepository.DeleteCartAsync(cart.Id); // Lógica para deletar o carrinho

        return Unit.Value; // Retorna um valor vazio
    }
}
using Ambev.DeveloperEvaluation.Application.Products.Commands;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.Id);
        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }

        await _productRepository.DeleteProductAsync(product.Id); // Chame o método de deleção passando o ID do produto

        return Unit.Value; // Retorna um valor vazio
    }
}
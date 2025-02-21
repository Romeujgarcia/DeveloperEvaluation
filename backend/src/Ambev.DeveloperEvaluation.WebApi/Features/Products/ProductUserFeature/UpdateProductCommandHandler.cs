using Ambev.DeveloperEvaluation.Application.Models;
using Ambev.DeveloperEvaluation.Application.Products.Commands;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // Lógica para atualizar o produto
        var product = await _productRepository.GetProductByIdAsync(request.Id);
        if (product == null)
        {
            return null; // Ou lance uma exceção, dependendo da sua lógica
        }

        // Atualize as propriedades do produto
        product.Title = request.Title;
        product.Price = request.Price;
        product.Description = request.Description;
        product.Category = request.Category;
        product.Image = request.Image;

        await _productRepository.UpdateProductAsync(product);

        // Retorne o ProductDto atualizado
        return new ProductDto
        {
            Id = product.Id,
            Title = product.Title,
            Price = product.Price,
            Description = product.Description,
            Category = product.Category,
            Image = product.Image
        };
    }
}
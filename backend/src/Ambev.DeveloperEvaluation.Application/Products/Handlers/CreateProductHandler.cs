// Handlers/CreateProductHandler.cs
using MediatR;
using Ambev.DeveloperEvaluation.Application.Products.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Title = request.Title,
                Price = request.Price,
                Description = request.Description,
                Category = request.Category,
                Image = request.Image,
                Rating = request.Rating != null ? new Rating
            {
                Rate = request.Rating.Rate,
                Count = request.Rating.Count
            } : null // Se Rating for nulo, não atribui
            };

            await _productRepository.AddProductAsync(product); // Adiciona o produto ao repositório
            return product; // Retorna o produto criado
        }
    }
}
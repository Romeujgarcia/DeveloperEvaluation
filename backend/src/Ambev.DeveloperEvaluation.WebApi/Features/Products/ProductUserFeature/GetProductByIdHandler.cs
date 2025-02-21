using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Products.Queries;
using Ambev.DeveloperEvaluation.Application.Models;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id);
            if (product == null) return null;

            // Mapeia a entidade Product para o DTO ProductDto
            return new ProductDto
            {
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                Image = product.Image,
                Rating = new RatingDto
                {
                    Rate = product.Rating?.Rate ?? 0,
                    Count = product.Rating?.Count ?? 0
                }
            };
        }
    }
}
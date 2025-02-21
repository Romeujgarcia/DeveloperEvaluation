using Ambev.DeveloperEvaluation.Application.Products.Queries;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProductsAsync(request.Page, request.Size, request.Order, request.Category, request.MinPrice, request.MaxPrice, request.Title); // Obtenha todos os produtos com base nos parâmetros de consulta
            var totalItems = await _productRepository.GetTotalProductsCountAsync(); // Obtenha a contagem total de produtos

            return new ProductResponse
            {
                Data = new ProductData
                {
                    Values = products.ToList() // Converta para lista se necessário
                },
                TotalItems = totalItems,
                CurrentPage = request.Page,
                TotalPages = (int)Math.Ceiling((double)totalItems / request.Size)
            };
        }
    }
}
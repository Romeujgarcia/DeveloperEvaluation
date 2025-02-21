using MediatR;
using Ambev.DeveloperEvaluation.Application.Products.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities; // Certifique-se de que o namespace está correto

namespace Ambev.DeveloperEvaluation.Application.Products.Commands
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
        public RatingRequest? Rating { get; set; } // Referência à classe RatingRequest
    }

    public class RatingRequest
    {
        public decimal Rate { get; set; }
        public int Count { get; set; }
    }
}
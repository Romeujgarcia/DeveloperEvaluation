using Ambev.DeveloperEvaluation.Application.Models;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands
{
    public class UpdateProductCommand : IRequest<ProductDto>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
        public RatingRequest? Rating { get; set; }
    }
}
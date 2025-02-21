using Ambev.DeveloperEvaluation.Application.Models;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}
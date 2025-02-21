using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { get; set; }
    }
}
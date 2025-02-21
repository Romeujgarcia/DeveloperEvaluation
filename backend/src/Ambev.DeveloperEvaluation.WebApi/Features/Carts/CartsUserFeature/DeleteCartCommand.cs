using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands
{
    public class DeleteCartCommand : IRequest
    {
        public int Id { get; set; }
    }
}
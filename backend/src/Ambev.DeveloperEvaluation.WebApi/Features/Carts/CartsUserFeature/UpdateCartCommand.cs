using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands
{
    public class UpdateCartCommand : IRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Adicione esta linha
        public DateTime Date { get; set; } // Adicione esta linha
        public List<CartProductRequest>? Products { get; set; } = new List<CartProductRequest>(); // Inicializa com uma lista vazia
    }
}
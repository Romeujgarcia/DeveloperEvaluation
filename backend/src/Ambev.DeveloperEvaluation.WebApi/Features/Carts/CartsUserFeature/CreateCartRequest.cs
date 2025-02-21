namespace Ambev.DeveloperEvaluation.Application.Carts
{
    public class CreateCartRequest
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CartProductRequest> Products { get; set; } = new List<CartProductRequest>(); // Inicializa com uma lista vazia
    }

    public class CartProductRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
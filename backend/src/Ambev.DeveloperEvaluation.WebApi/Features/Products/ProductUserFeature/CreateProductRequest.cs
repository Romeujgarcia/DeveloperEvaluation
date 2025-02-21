namespace Ambev.DeveloperEvaluation.Application.Products.Commands
{
    public class CreateProductRequest
    {
        public string Title { get; set; } = string.Empty; // Inicializa com uma string vazia
        public decimal? Price { get; set; }
        public string Description { get; set; } = string.Empty; // Inicializa com uma string vazia
        public string Category { get; set; } = string.Empty; // Inicializa com uma string vazia
        public string Image { get; set; } = string.Empty; // Inicializa com uma string vazia
        public RatingRequest? Rating { get; set; }
    }


}
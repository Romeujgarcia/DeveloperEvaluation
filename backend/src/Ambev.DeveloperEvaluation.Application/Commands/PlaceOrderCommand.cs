
public class PlaceOrderCommand
{
    public string? OrderId { get; set; }
    public Guid UserId { get; set; } // ID do usuário que está fazendo o pedido
    public List<OrderProduct> Products { get; set; } = new List<OrderProduct>(); // Lista de produtos no pedido
    public string Title { get; set; } = string.Empty; // Título do produto
    public decimal Price { get; set; } // Preço do produto
    public string Description { get; set; } = string.Empty; // Descrição do produto
    public string Category { get; set; } = string.Empty; // Categoria do produto
    public string Image { get; set; } = string.Empty; // Imagem do produto
    public Rating Rating { get; set; } = new Rating(); // Avaliação do produto
}

public class OrderProduct
{
    public int Id { get; set; } // ID do produto
    public int Quantity { get; set; } // Quantidade do produto
}

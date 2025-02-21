using Ambev.DeveloperEvaluation.Domain.Entities;

public class ProductResponse
{
    public ProductData Data { get; set; }
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}

public class ProductData
{
    public List<Product> Values { get; set; }
}
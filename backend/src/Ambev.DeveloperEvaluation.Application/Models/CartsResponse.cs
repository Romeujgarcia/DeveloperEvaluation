using Ambev.DeveloperEvaluation.Application.Models;

public class CartsResponse
{
    public List<CartDto> Data { get; set; }
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
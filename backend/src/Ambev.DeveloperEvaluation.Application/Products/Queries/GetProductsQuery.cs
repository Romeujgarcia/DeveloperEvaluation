// Queries/GetProductsQuery.cs
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<ProductResponse>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string Order { get; set; } = string.Empty;
        public string? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Title { get; set; }
        public string? TitleWildcard { get; set; } // Para suporte a caracteres curinga
    }
}
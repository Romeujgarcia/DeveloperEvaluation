// Product.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public string? Image { get; set; }
        
       [NotMapped] // Ignora a propriedade Rating
        public Rating? Rating { get; set; }
    }

}
using System.ComponentModel.DataAnnotations;
using Ambev.DeveloperEvaluation.Domain.Entities;

public class CartProduct
    {
        [Key] // Adicione esta anotação para definir a chave primária
        public int Id { get; set; } // Adicione uma propriedade Id como chave primária
        [Required(ErrorMessage = "ProductId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ProductId must be greater than 0.")]
        public int ProductId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        // Adicione uma propriedade de navegação se necessário
        
        public int CartId { get; set; } // Chave estrangeira para Cart
        public Cart? Cart { get; set; } // Se CartProduct pertence a um Cart
    }
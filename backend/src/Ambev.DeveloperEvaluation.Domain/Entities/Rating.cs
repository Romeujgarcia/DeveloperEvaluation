
using System.ComponentModel.DataAnnotations;
using Ambev.DeveloperEvaluation.Domain.Entities;

public class Rating
    {
        [Key] // Adicione esta anotação para definir a chave primária
        public int Id { get; set; } 
        public decimal Rate { get; set; }
        public int Count { get; set; }

        // Adicione uma propriedade de navegação se necessário
        public int ProductId { get; set; } // Chave estrangeira para Product
        public Product? Product { get; set; } // Propriedade de navegação para Product
    }
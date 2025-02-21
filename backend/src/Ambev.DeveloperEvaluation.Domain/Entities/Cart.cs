// Cart.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Cart
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "User   Id is required.")]
    [NotZero(ErrorMessage = "User  Id must be greater than zero.")]
    [Range(1, int.MaxValue, ErrorMessage = "User Id must be greater than 0.")]
    public int UserId { get; set; } // Mantido como int
    
    [Required(ErrorMessage = "Date is required.")]
    public DateTime Date { get; set; }

    [NotMapped]
    [JsonIgnore] [Required(ErrorMessage = "Products are required.")]
    public List<CartProduct>? Products { get; set; } = new List<CartProduct>();
    
}
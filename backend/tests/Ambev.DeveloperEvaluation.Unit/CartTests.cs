using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

public class CartTests
{
    [Fact]
    public void Cart_ShouldInitializeProductsList()
    {
        // Arrange
        var cart = new Cart();

        // Act
        var products = cart.Products;

        // Assert
        Assert.NotNull(products);
        Assert.Empty(products); // A lista deve estar vazia ao ser inicializada
    }

    [Fact]
    public void Cart_ShouldHaveRequiredUserId()
    {
        // Arrange
        var cart = new Cart { UserId =  -1}; // UserId deve ser 0 para testar a validação

        // Act
        var validationResults = ValidateCart(cart);

        // Assert
        Assert.Contains(validationResults, r => r.ErrorMessage.Contains("User  Id must be greater than zero."));
    }

    private List<ValidationResult> ValidateCart(Cart cart)
    {
        var context = new ValidationContext(cart);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(cart, context, results, true);
        return results;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

public class CartProductTests
{
    [Fact]
    public void CartProduct_ShouldHaveValidQuantity()
    {
        // Arrange
        var cartProduct = new CartProduct { Quantity = -1 }; // Quantidade inválida

        // Act & Assert
        var validationResults = ValidateCartProduct(cartProduct);
        Assert.Contains(validationResults, r => r.ErrorMessage.Contains("Quantity must be greater than zero."));
    }

    private List<ValidationResult> ValidateCartProduct(CartProduct cartProduct)
    {
        var context = new ValidationContext(cartProduct);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(cartProduct, context, results, true);
        return results;
    }
}
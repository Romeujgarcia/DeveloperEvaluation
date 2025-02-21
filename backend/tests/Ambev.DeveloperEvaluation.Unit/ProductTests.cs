using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

public class ProductTests
{
    [Fact]
    public void Product_ShouldHaveRequiredTitle()
    {
        // Arrange
        var product = new Product { Title = null }; // Título não pode ser nulo

        // Act & Assert
        var validationResults = ValidateProduct(product);
        Assert.Contains(validationResults, r => r.ErrorMessage.Contains("The Title field is required."));
    }

    [Fact]
    public void Product_ShouldHavePositivePrice()
    {
        // Arrange
        var product = new Product { Price = -1 }; // Preço inválido

        // Act & Assert
        var validationResults = ValidateProduct(product);
        Assert.Contains(validationResults, r => r.ErrorMessage.Contains("Price must be a positive number."));
    }

    private List<ValidationResult> ValidateProduct(Product product)
    {
        var context = new ValidationContext(product);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(product, context, results, true);
        return results;
    }
}
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Products.Queries;
using Ambev.DeveloperEvaluation.Application.Products.Handlers;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Products.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Models;

public class ProductsControllerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ProductsController _controller; // Substitua pelo nome do seu controlador
    private readonly GetProductsHandler _handler;

    public ProductsControllerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _mediatorMock = new Mock<IMediator>();
        _handler = new GetProductsHandler(_productRepositoryMock.Object);
        _controller = new ProductsController(_mediatorMock.Object); // Inicialize o controlador com o mock do Mediator
    }

    [Fact]
    public async Task GetProductsHandler_ReturnsProducts()
    {
        // Arrange
        var query = new GetProductsQuery { Page = 1, Size = 10, Order = "Title" };
        var products = new List<Product>
        {
            new Product { Title = "Produto Exemplo", Id = 1, Price = 100, Description = "Descrição do produto", Category = "Categoria Exemplo", Image = "url_da_imagem" }
        };

        // Configure o mock para retornar a lista de produtos e a contagem total
        _productRepositoryMock.Setup(m => m.GetAllProductsAsync(query.Page, query.Size, query.Order)).ReturnsAsync(products);
        _productRepositoryMock.Setup(m => m.GetTotalProductsCountAsync()).ReturnsAsync(products.Count); // Retorna 1

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.Values);
        Assert.Single(result.Data.Values); // Verifique se há um produto na lista
        Assert.Equal(1, result.TotalItems); // Verifique se a contagem total de itens é 1
        Assert.Equal("Produto Exemplo", result.Data.Values[0].Title); // Verifique o título do produto
    }

    [Fact]
public async Task UpdateProduct_ReturnsOkResult_WithUpdatedProduct()
{
    // Arrange
    int productId = 1;
    var request = new CreateProductRequest
    {
        Title = "Produto Atualizado",
        Price = 150,
        Description = "Descrição atualizada",
        Category = "Categoria Atualizada",
        Image = "nova_url_da_imagem"
    };

    var updatedProductDto = new ProductDto
    {
        Id = productId,
        Title = request.Title,
        Price = request.Price ?? 0,
        Description = request.Description,
        Category = request.Category,
        Image = request.Image
    };

    // Configure o mock para retornar o produto atualizado
    _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), default)).ReturnsAsync(updatedProductDto);

    // Act
    var result = await _controller.UpdateProduct(productId, request);

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result);
    var returnValue = Assert.IsType<ProductDto>(okResult.Value);
    Assert.Equal(updatedProductDto.Id, returnValue.Id);
    Assert.Equal(updatedProductDto.Title, returnValue.Title);
}

    [Fact]
    public async Task DeleteProduct_ReturnsNoContent()
    {
        // Arrange
        int productId = 1;
      _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default)).Returns(Task.FromResult(Unit.Value));

        // Act
        var result = await _controller.DeleteProduct(productId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
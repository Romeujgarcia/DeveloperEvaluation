using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Carts.Commands;
using Ambev.DeveloperEvaluation.Application.Carts.Queries;
using Ambev.DeveloperEvaluation.Application.Carts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Models;
using Microsoft.AspNetCore.Http;

public class CartsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CartsController _controller;

    public CartsControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new CartsController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetCarts_ReturnsOkResult_WithCarts()
    {
        // Arrange
        var carts = new List<CartDto>
    {
        new CartDto { Id = 1, UserId = 1, Date = DateTime.Now, Products = new List<CartProductDto>() },
        new CartDto { Id = 2, UserId = 2, Date = DateTime.Now, Products = new List<CartProductDto>() }
    };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetCartsQuery>(), default)).ReturnsAsync(carts);

        // Act
        var result = await _controller.GetCarts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<CartsResponse>(okResult.Value); // Verifique a nova classe de resposta
        Assert.Equal(carts.Count, returnValue.Data.Count);
        Assert.Equal(1, returnValue.CurrentPage);
        Assert.Equal((int)Math.Ceiling((double)carts.Count / 10), returnValue.TotalPages);
        Assert.Equal(carts.Count, returnValue.TotalItems);
    }

    [Fact]
    public async Task CreateCart_ReturnsCreatedAtAction_WithCart()
    {
        // Arrange
        var request = new CreateCartRequest
        {
            UserId = 1,
            Date = DateTime.Now,
            Products = new List<CartProductRequest>
        {
            new CartProductRequest { ProductId = 1, Quantity = 2 },
            new CartProductRequest { ProductId = 2, Quantity = 3 }
        }
        };

        var createdCart = new CartDto
        {
            Id = 1,
            UserId = request.UserId,
            Date = request.Date,
            Products = new List<CartProductDto>
        {
            new CartProductDto { ProductId = 1, Quantity = 2 },
            new CartProductDto { ProductId = 2, Quantity = 3 }
        }
        };

        // Configurando o mock para retornar CartDto
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCartCommand>(), default)).ReturnsAsync(createdCart);

        // Act
        var result = await _controller.CreateCart(request);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(CartsController.GetCartById), createdResult.ActionName);
        Assert.Equal(1, createdResult.RouteValues["id"]);
        Assert.Equal(createdCart, createdResult.Value);
    }

    [Fact]
    public async Task GetCartById_ReturnsOkResult_WithCartDto()
    {
        // Arrange
        int cartId = 1;
        var cartDto = new CartDto
        {
            Id = cartId,
            UserId = 1,
            Date = DateTime.Now,
            Products = new List<CartProductDto>()
        };

        // Configurando o mock para retornar o CartDto
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetCartByIdQuery>(), default)).ReturnsAsync(cartDto);

        // Act
        var result = await _controller.GetCartById(cartId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<CartDto>(okResult.Value);
        Assert.Equal(cartId, returnValue.Id);
        Assert.Equal(1, returnValue.UserId);
    }

    [Fact]
    public async Task UpdateCart_ReturnsOkResult_WithUpdatedCart()
    {
        // Arrange
        int cartId = 1;
        var request = new CreateCartRequest
        {
            UserId = 1,
            Date = DateTime.Now,
            Products = new List<CartProductRequest>
        {
            new CartProductRequest { ProductId = 1, Quantity = 2 },
            new CartProductRequest { ProductId = 2, Quantity = 3 }
        }
        };

        var updatedCart = new CartDto
        {
            Id = cartId,
            UserId = request.UserId,
            Date = request.Date,
            Products = new List<CartProductDto>
        {
            new CartProductDto { ProductId = 1, Quantity = 2 },
            new CartProductDto { ProductId = 2, Quantity = 3 }
        }
        };

        // Configurando o mock para retornar o CartDto atualizado
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateCartCommand>(), default))
                      .ReturnsAsync(Unit.Value); // Retorna Unit

        // Configurando o mock para retornar o CartDto ao buscar pelo ID
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetCartByIdQuery>(), default))
                      .ReturnsAsync(updatedCart); // Retorna o carrinho atualizado

        // Act
        var result = await _controller.UpdateCart(cartId, request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<CartDto>(okResult.Value);
        Assert.Equal(cartId, returnValue.Id);
        Assert.Equal(request.UserId, returnValue.UserId);
        Assert.Equal(request.Date, returnValue.Date);
        Assert.Equal(2, returnValue.Products.Count);
        Assert.Equal(1, returnValue.Products[0].ProductId);
        Assert.Equal(2, returnValue.Products[0].Quantity);
        Assert.Equal(2, returnValue.Products[1].ProductId);
        Assert.Equal(3, returnValue.Products[1].Quantity);
    }

    [Fact]
public async Task DeleteCart_ReturnsNoContent()
{
    // Arrange
    int cartId = 1;
    var command = new DeleteCartCommand { Id = cartId };

    // Configurando o mock para o comando de exclusão
    _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCartCommand>(), default)).ReturnsAsync(Unit.Value);

    // Act
    var result = await _controller.DeleteCart(cartId);

    // Assert
    var noContentResult = Assert.IsType<NoContentResult>(result);
    Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
}

}
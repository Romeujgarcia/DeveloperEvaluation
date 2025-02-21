using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

public class OrderControllerTests
{
    private readonly Mock<IOrderService> _orderServiceMock;
    private readonly Mock<ILogger<OrderController>> _loggerMock;
    private readonly OrderController _controller;

    public OrderControllerTests()
    {
        _orderServiceMock = new Mock<IOrderService>();
        _loggerMock = new Mock<ILogger<OrderController>>();
        _controller = new OrderController(_orderServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateOrder_ReturnsOk_WhenOrderIsPlacedSuccessfully()
    {
        // Arrange
        var command = new PlaceOrderCommand { OrderId = 1.ToString() }; // Supondo que OrderId é um inteiro

        // Act
        var result = await _controller.CreateOrder(command);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Order placed successfully.", okResult.Value);
        _orderServiceMock.Verify(service => service.PlaceOrder(command), Times.Once);
    }

    [Fact]
    public async Task CreateOrder_ReturnsInternalServerError_WhenExceptionIsThrown()
    {
        // Arrange
        var command = new PlaceOrderCommand { OrderId = 1.ToString() };
        _orderServiceMock.Setup(service => service.PlaceOrder(command)).ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.CreateOrder(command);

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        Assert.Equal("Internal server error", statusCodeResult.Value);
        _orderServiceMock.Verify(service => service.PlaceOrder(command), Times.Once);
    }
}
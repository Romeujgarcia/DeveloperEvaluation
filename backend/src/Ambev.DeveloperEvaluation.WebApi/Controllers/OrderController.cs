using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderService orderService, ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] PlaceOrderCommand command)
    {
        _logger.LogInformation("Received request to create order with ID: {OrderId}", command.OrderId);
        try
        {
            await _orderService.PlaceOrder(command);
            _logger.LogInformation("Order with ID: {OrderId} created successfully.", command.OrderId);
            return Ok("Order placed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating order with ID: {OrderId}", command.OrderId);
            return StatusCode(500, "Internal server error");
        }
    }
}
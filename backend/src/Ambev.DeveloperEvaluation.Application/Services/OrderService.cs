using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Rebus.Bus;

public class OrderService : IOrderService
{
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBus _bus;
    
    private readonly ILogger<OrderService> _logger;

    public OrderService(IProductRepository productRepository, IUserRepository userRepository, IBus bus, ILogger<OrderService> logger)
    {
        _productRepository = productRepository;
        _userRepository = userRepository;
        _bus = bus;
        _logger = logger;
    }           

    public async Task PlaceOrder(PlaceOrderCommand command)
    {
         _logger.LogInformation("Placing order with ID: {OrderId}", command.OrderId);
        // Validação do usuário
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user == null || user.Status != UserStatus.Active)
        {
            throw new InvalidOperationException("User  is not authorized to place an order.");
        }

        // Validação dos produtos
        if (command.Products == null || !command.Products.Any())
        {
            throw new InvalidOperationException("At least one product must be included in the order.");
        }

        foreach (var product in command.Products)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(product.Id);
            if (existingProduct == null)
            {
                throw new InvalidOperationException($"Product with ID {product.Id} does not exist.");
            }

            if (product.Quantity <= 0)
            {
                throw new InvalidOperationException("Product quantity must be greater than zero.");
            }
            
        }

        // Lógica para calcular o total do pedido e enviar o comando
        await _bus.Send(command);
         _logger.LogInformation("Order with ID: {OrderId} placed successfully.", command.OrderId);
    }
}
// Ambev.DeveloperEvaluation.Application/Services/IOrderService.cs

public interface IOrderService
{
    Task PlaceOrder(PlaceOrderCommand command); // Altere para PlaceOrderCommand
}
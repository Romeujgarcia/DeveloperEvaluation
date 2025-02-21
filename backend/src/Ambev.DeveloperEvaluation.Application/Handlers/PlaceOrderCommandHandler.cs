
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Rebus.Handlers;
using System;
using System.Threading.Tasks;

public class PlaceOrderCommandHandler : IHandleMessages<PlaceOrderCommand>
{
    private readonly IProductRepository _productRepository; // Repositório para produtos
    private readonly IUserRepository _userRepository; // Repositório para usuários

    public PlaceOrderCommandHandler(IProductRepository productRepository, IUserRepository userRepository)
    {
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(PlaceOrderCommand message)
    {
        // Verifica o status do usuário
        var user = await _userRepository.GetByIdAsync(message.UserId); // Obtém o usuário pelo ID
         if (user == null || user.Status != UserStatus.Active) // Verifica se o usuário existe e está ativo // Verifica se o usuário existe e está ativo
        {
            throw new InvalidOperationException("User  is not authorized to create a product.");
        }

        // Cria o produto
        var product = new Product
        {
            Title = message.Title,
            Price = message.Price,
            Description = message.Description,
            Category = message.Category,
            Image = message.Image,
            Rating = new Rating
            {
                Rate = message.Rating.Rate,
                Count = message.Rating.Count
            }
        };

        // Salva o produto no banco de dados
        await _productRepository.AddProductAsync(product);
    }
}
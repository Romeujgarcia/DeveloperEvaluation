using Ambev.DeveloperEvaluation.Application.Models;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries
{
    public class GetCartByIdQuery : IRequest<CartDto>
{
    public int Id { get; set; }

    // Construtor que aceita um ID
    public GetCartByIdQuery(int id)
    {
        Id = id;
    }
}
}
// Commands/CreateCartCommand.cs
using Ambev.DeveloperEvaluation.Application.Models;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands
{
    public class CreateCartCommand : IRequest<CartDto>
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CartProduct>? Products { get; set; } = new List<CartProduct>();
    }
}
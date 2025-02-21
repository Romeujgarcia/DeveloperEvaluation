using Ambev.DeveloperEvaluation.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries
{
    public class GetCartsQuery : IRequest<List<CartDto>> // ou IRequest<IEnumerable<CartDto>>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string? Order { get; set; }
    }
}
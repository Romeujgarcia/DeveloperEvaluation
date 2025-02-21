using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Carts.Queries;
using Ambev.DeveloperEvaluation.Application.Carts.Commands;
using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Application.Models;
using Ambev.DeveloperEvaluation.Domain.Entities;



[ApiController]
[Route("carts")]
public class CartsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCarts([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string order = "")
    {
        var query = new GetCartsQuery { Page = page, Size = size, Order = order };
        var result = await _mediator.Send(query);

        var totalItems = result.Count();

        // Verifique se há itens
        if (totalItems == 0)
        {
            return Ok(new CartsResponse
            {
                Data = new List<CartDto>(), // Retorna uma lista vazia
                TotalItems = totalItems,
                CurrentPage = page,
                TotalPages = 0
            });
        }

        var response = new CartsResponse
        {
            Data = result.Select(cart => new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Date = cart.Date,
                Products = cart.Products.Select(p => new CartProductDto
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList()
            }).ToList(),
            TotalItems = totalItems,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling((double)totalItems / size)
        };

        return Ok(response);
    }


   [HttpPost]
public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest request)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState); // Retorna os erros de validação
    }

    var cartProducts = new List<CartProduct>();
    foreach (var productRequest in request.Products)
    {
        cartProducts.Add(new CartProduct
        {
            ProductId = productRequest.ProductId,
            Quantity = productRequest.Quantity
        });
    }

    var command = new CreateCartCommand
    {
        UserId = request.UserId,
        Date = request.Date,
        Products = cartProducts
    };

    var result = await _mediator.Send(command);
    return CreatedAtAction(nameof(GetCartById), new { id = result.Id }, result);
}


    [HttpGet("{id}")]
    public async Task<IActionResult> GetCartById(int id)
    {
        var query = new GetCartByIdQuery(id);
        var result = await _mediator.Send(query);
        if (result == null)
        {
            return NotFound(); // Retorna 404 se o carrinho não for encontrado
        }
        
        
        return Ok(result);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCart(int id, [FromBody] CreateCartRequest request)
    {
        var command = new UpdateCartCommand
        {
            Id = id,
            UserId = request.UserId,
            Date = request.Date,
            Products = request.Products
        };

        await _mediator.Send(command); // Apenas envia o comando

        // Aqui você pode buscar o carrinho atualizado
        var updatedCart = await _mediator.Send(new GetCartByIdQuery(id)); // Passando o id para o construtor

        return Ok(updatedCart); // Retorna o carrinho atualizado
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCart(int id)
    {
        var command = new DeleteCartCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
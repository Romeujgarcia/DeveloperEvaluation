using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Products.Queries;
using Ambev.DeveloperEvaluation.Application.Products.Commands;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(
    [FromQuery] int page = 1,
    [FromQuery] int size = 10,
    [FromQuery] string order = "",
    [FromQuery] string? category = null,
    [FromQuery] decimal? minPrice = null,
    [FromQuery] decimal? maxPrice = null,
    [FromQuery] string? title = null,
    [FromQuery] string? titleWildcard = null)
    {
        // Validação dos parâmetros
        if (size <= 0)
        {
            return BadRequest(new ErrorResponse("ValidationError", "Invalid input data", "Size must be greater than zero."));
        }

        if (page <= 0)
        {
            return BadRequest(new ErrorResponse("ValidationError", "Invalid input data", "Page must be greater than zero."));
        }

        var query = new GetProductsQuery
        {
            Page = page,
            Size = size,
            Order = order,
            Category = category,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            Title = title,
            TitleWildcard = titleWildcard
        };

        var result = await _mediator.Send(query);

        if (result == null || !result.Data.Values.Any())
        {
            return NotFound(new ErrorResponse("ResourceNotFound", "Product not found", "No products match the specified criteria."));
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        var command = new CreateProductCommand
        {
            Title = request.Title,
            Price = request.Price ?? 0,
            Description = request.Description,
            Category = request.Category,
            Image = request.Image,
            Rating = request.Rating
        };
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var query = new GetProductByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        if (result == null)
        {
            return NotFound(new { error = "Product not found" });
        }
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateProductRequest request)
    {
        var command = new UpdateProductCommand
        {
            Id = id,
            Title = request.Title,
            Price = request.Price ?? 0,
            Description = request.Description,
            Category = request.Category,
            Image = request.Image,
            Rating = request.Rating
        };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var command = new DeleteProductCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
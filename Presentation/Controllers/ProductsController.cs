using Application.Products.CreateProduct;
using Application.Products.GetProduct;
using Application.Products.GetProducts;
using Domain.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Contracts.Products;

namespace Presentation.Controllers;

[Route("api/products")]
public sealed class ProductsController : ApiController
{
    public ProductsController(ISender sender)
            : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        Result<List<ProductsResponse>> response = await Sender.Send(new GetProductsQuery(), cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        Result<ProductResponse> response = await Sender.Send(new GetProductByIdQuery(id), cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
    [FromBody] CreateProductRequest request,
    CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateProductCommand>();

        Result<Guid> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value },
            result.Value);
    }
}

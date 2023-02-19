using Aplication.ProductCategories.GetProductCategory;
using Application.ProductCategories.CreateProductCategory;
using Application.ProductCategories.GetProductCategories;
using Application.ProductCategories.GetProductCategory;
using Application.ProductCategories.UpdateProductCategory;
using Domain.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Contracts.ProductCategories;

namespace Presentation.Controllers;

[Route("api/product-categories")]
public sealed class ProductCategoriesController : ApiController
{
    public ProductCategoriesController(ISender sender)
            : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        Result<List<ProductCategoriesResponse>> response = await Sender.Send(new GetProductCategoriesQuery(), cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        Result<ProductCategoryResponse> response = await Sender.Send(new GetProductCategoryByIdQuery(id), cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
    [FromBody] CreateProductCategoryRequest request,
    CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateProductCategoryCommand>();

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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
    Guid id,
    [FromBody] UpdateProductCategoryRequest request,
    CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest("Identifier mismatch error");
        }

        var command = request.Adapt<UpdateProductCategoryCommand>();

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
}

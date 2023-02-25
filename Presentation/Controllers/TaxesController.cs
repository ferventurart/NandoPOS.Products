using Application.Taxes.CreateTax;
using Application.Taxes.DeleteTax;
using Application.Taxes.GetTax;
using Application.Taxes.GetTaxes;
using Application.Taxes.UpdateTax;
using Domain.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Contracts.Taxes;

namespace Presentation.Controllers;

[Route("api/taxes")]
public sealed class TaxesController : ApiController
{
    public TaxesController(ISender sender)
            : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        Result<List<TaxesResponse>> response = await Sender.Send(new GetTaxesQuery(), cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        Result<TaxResponse> response = await Sender.Send(new GetTaxByIdQuery(id), cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
    [FromBody] CreateTaxRequest request,
    CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateTaxCommand>();

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
    [FromBody] UpdateTaxRequest request,
    CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest("Identifier mismatch error");
        }

        var command = request.Adapt<UpdateTaxCommand>();

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
    Guid id,
    CancellationToken cancellationToken)
    {
        var command = new DeleteTaxCommand(id);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
}

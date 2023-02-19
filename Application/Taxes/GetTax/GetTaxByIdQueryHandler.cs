using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.Taxes.GetTax;

internal sealed class GetTaxByIdQueryHandler
    : IQueryHandler<GetTaxByIdQuery, TaxResponse>
{
    private readonly IQuerySession _session;

    public GetTaxByIdQueryHandler(IQuerySession session)
    {
        _session = session;
    }

    public async Task<Result<TaxResponse>> Handle(GetTaxByIdQuery request, CancellationToken cancellationToken)
    {
        var tax = await _session.LoadAsync<Tax>(request.Id, cancellationToken);

        if (tax is null)
        {
            return Result.Failure<TaxResponse>(new Error(
                    "Tax.NotFound",
                    $"The record with the id {request.Id} was not found"));
        }

        var response = new TaxResponse(tax.Id, tax.Name, tax.ShortName, tax.Percentage);

        return Result.Success(response);
    }
}

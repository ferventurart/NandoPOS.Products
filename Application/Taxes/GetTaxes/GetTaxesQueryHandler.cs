using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.Taxes.GetTaxes;

internal sealed class GetTaxesQueryHandler
    : IQueryHandler<GetTaxesQuery, List<TaxesResponse>>
{
    private readonly IQuerySession _session;

    public GetTaxesQueryHandler(IQuerySession session)
    {
        _session = session;
    }

    public async Task<Result<List<TaxesResponse>>> Handle(GetTaxesQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<TaxesResponse> taxes = await _session
            .Query<Tax>()
            .Select(t => new TaxesResponse(
                t.Id,
                t.Name,
                t.ShortName,
                t.Percentage))
            .ToListAsync(cancellationToken);

        return taxes.ToList();
    }
}

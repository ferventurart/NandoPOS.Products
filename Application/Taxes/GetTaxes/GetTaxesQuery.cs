using Application.Abstractions.Messaging;

namespace Application.Taxes.GetTaxes;

public sealed record GetTaxesQuery : IQuery<List<TaxesResponse>>;

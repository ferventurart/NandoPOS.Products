using Application.Abstractions.Messaging;

namespace Application.Taxes.GetTax;

public sealed record GetTaxByIdQuery(Guid Id) : IQuery<TaxResponse>;

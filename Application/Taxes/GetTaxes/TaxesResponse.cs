namespace Application.Taxes.GetTaxes;

public sealed record TaxesResponse(
    Guid Id,
    string Name,
    string ShortName,
    decimal Percentage);

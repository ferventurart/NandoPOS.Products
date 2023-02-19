namespace Application.Taxes.GetTax;

public sealed record TaxResponse(
    Guid Id,
    string Name,
    string ShortName,
    decimal Percentage);

namespace Presentation.Contracts.Taxes;

public sealed record UpdateTaxRequest(
    Guid Id,
    string Name,
    string ShortName,
    decimal Percentage);

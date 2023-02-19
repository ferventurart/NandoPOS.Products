namespace Presentation.Contracts.Taxes;

public sealed record CreateTaxRequest(
    string Name,
    string ShortName,
    decimal Percentage);

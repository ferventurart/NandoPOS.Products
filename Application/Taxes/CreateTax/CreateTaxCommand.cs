using Application.Abstractions.Messaging;

namespace Application.Taxes.CreateTax;

public record CreateTaxCommand(
    string Name,
    string ShortName,
    decimal Percentage) : ICommand<Guid>;

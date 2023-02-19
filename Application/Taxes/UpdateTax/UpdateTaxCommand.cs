using Application.Abstractions.Messaging;

namespace Application.Taxes.UpdateTax;

public record UpdateTaxCommand(
    Guid Id,
    string Name,
    string ShortName,
    decimal Percentage) : ICommand;

using Application.Abstractions.Messaging;

namespace Application.Taxes.DeleteTax;

public record DeleteTaxCommand(Guid Id) : ICommand;

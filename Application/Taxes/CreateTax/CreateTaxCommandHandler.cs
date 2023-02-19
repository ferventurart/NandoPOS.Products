using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.Taxes.CreateTax;

internal sealed class CreateTaxCommandHandler : ICommandHandler<CreateTaxCommand, Guid>
{
    private readonly IDocumentSession _session;

    public CreateTaxCommandHandler(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<Result<Guid>> Handle(CreateTaxCommand request, CancellationToken cancellationToken)
    {
        Tax tax = new(request.Name, request.ShortName, request.Percentage);

        _session.Store(tax);

        await _session.SaveChangesAsync(cancellationToken);

        return tax.Id;
    }
}

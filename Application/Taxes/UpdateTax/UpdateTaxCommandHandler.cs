using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.Taxes.UpdateTax;

internal sealed class UpdateTaxCommandHandler : ICommandHandler<UpdateTaxCommand>
{
    private readonly IDocumentSession _session;

    public UpdateTaxCommandHandler(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<Result> Handle(UpdateTaxCommand request, CancellationToken cancellationToken)
    {
        var tax = await _session.LoadAsync<Tax>(request.Id, cancellationToken);

        if (tax == null)
        {
            return Result.Failure(new Error(
                    "Tax.NotFound",
                    $"The record with the id {request.Id} was not found"));
        }

        tax.ChangeName(request.Name);
        tax.ChangeShortName(request.ShortName);
        tax.ChangePercentage(request.Percentage);

        _session.Update(tax);

        await _session.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.Taxes.DeleteTax;

public class DeleteTaxCommandHandler : ICommandHandler<DeleteTaxCommand>
{
    private readonly IDocumentSession _session;

    public DeleteTaxCommandHandler(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<Result> Handle(DeleteTaxCommand request, CancellationToken cancellationToken)
    {
        var tax = await _session.LoadAsync<Tax>(request.Id, cancellationToken);

        if (tax is null)
        {
            return Result.Failure(new Error(
                    "Tax.NotFound",
                    $"The record with the id {request.Id} was not found"));
        }

        var products = await _session.Query<Product>().ToListAsync(cancellationToken);

        if (products.Any(p => p.Taxes.Any(t => t.Id == request.Id)))
        {
            return Result.Failure(new Error(
                   "Tax.Assigned",
                   $"The record with the id {request.Id} cannot be deleted"));
        }

        _session.Delete<Tax>(request.Id);

        await _session.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

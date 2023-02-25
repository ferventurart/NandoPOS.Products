using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.ProductCategories.DeleteProductCategory;

public class DeleteProductCategoryCommandHandler : ICommandHandler<DeleteProductCategoryCommand>
{
    private readonly IDocumentSession _session;

    public DeleteProductCategoryCommandHandler(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<Result> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productCategory = await _session.LoadAsync<ProductCategory>(request.Id, cancellationToken);

        if (productCategory is null)
        {
            return Result.Failure(new Error(
                    "ProductCategory.NotFound",
                    $"The record with the id {request.Id} was not found"));
        }

        var products = await _session.Query<Product>().ToListAsync(cancellationToken);

        if (products.Any(p => p.ProductCategory.Id == request.Id))
        {
            return Result.Failure(new Error(
                   "ProductCategory.Assigned",
                   $"The record with the id {request.Id} cannot be deleted"));
        }

        _session.Delete<ProductCategory>(request.Id);

        await _session.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

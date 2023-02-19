using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.ProductCategories.UpdateProductCategory;

internal sealed class UpdateProductCategoryCommandHandler : ICommandHandler<UpdateProductCategoryCommand>
{
    private readonly IDocumentSession _session;

    public UpdateProductCategoryCommandHandler(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<Result> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productCategory = await _session.LoadAsync<ProductCategory>(request.Id, cancellationToken);

        if (productCategory == null)
        {
            return Result.Failure(new Error(
                    "ProductCategory.NotFound",
                    $"The record with the id {request.Id} was not found"));
        }

        productCategory.ChangeName(request.Name);

        if (productCategory.Active != request.Active)
        {
            productCategory.ChangeStatus();
        }

        _session.Update(productCategory);

        await _session.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

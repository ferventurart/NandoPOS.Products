using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.Products.UpdateProduct;

internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
{
    private readonly IDocumentSession _session;

    public UpdateProductCommandHandler(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _session.LoadAsync<Product>(request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure(new Error(
                    "Product.NotFound",
                    $"The record with the id {request.Id} was not found"));
        }

        product.ChangeBarcode(request.Barcode);
        product.ChangeSku(request.Sku);
        product.ChangeName(request.Name);
        product.ChangeDescription(request.Description);
        product.ChangeCost(request.Cost);
        product.ChangePrice(request.Price);
        product.CalculateEarn();
        product.ChangeStockMin(request.StockMin);
        product.ChangeStockMax(request.StockMax);

        if (request.ProductCategoryId != product.ProductCategory.Id)
        {
            var productCategory = await _session.LoadAsync<ProductCategory>(request.ProductCategoryId, cancellationToken);

            if (productCategory is not null)
            {
                product.ChangeProductCategory(productCategory);
            }
        }

        if (product.Active != request.Active)
        {
            product.ChangeStatus();
        }

        if (product.UseInventory != request.UseInventory)
        {
            product.ChangeUseInventory();
        }

        product.AddSizes(request.Sizes);
        product.AddColors(request.Colors);

        var taxes = await _session.Query<Tax>().ToListAsync(cancellationToken);
        product.AddTaxes(taxes, request.Taxes);

        _session.Update(product);

        await _session.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

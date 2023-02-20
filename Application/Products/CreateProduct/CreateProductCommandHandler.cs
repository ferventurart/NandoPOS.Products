using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.Products.CreateProduct;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IDocumentSession _session;

    public CreateProductCommandHandler(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ProductCategory? productCategory = await _session.LoadAsync<ProductCategory>(request.ProductCategoryId, cancellationToken);

        if (productCategory is null)
        {
            return Result.Failure<Guid>(new Error(
                "Product.CategoryNotValid",
                $"The Product Category Id {request.ProductCategoryId} is not valid"));
        }

        Product product = new(
            request.Barcode,
            request.Sku,
            request.Name,
            request.Description,
            request.Cost,
            request.Price,
            request.UseInventory,
            productCategory);

        if (request.Sizes.Count > 0)
        {
            foreach (var size in request.Sizes)
            {
                product.AddSize(size);
            }
        }

        if (request.Colors.Count > 0)
        {
            foreach (var color in request.Colors)
            {
                product.AddColor(color);
            }
        }

        if (request.Taxes.Count > 0)
        {
            foreach (var taxId in request.Taxes)
            {
                Tax? tax = await _session.LoadAsync<Tax>(taxId, cancellationToken);
                if (tax is not null)
                {
                    product.AddTax(tax);
                }
            }
        }

        _session.Store<Product>(product);

        await _session.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}

using Aplication.ProductCategories.GetProductCategory;
using Application.Abstractions.Messaging;
using Application.Taxes.GetTax;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.Products.GetProduct;

internal sealed class GetProductByIdQueryHandler
    : IQueryHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IQuerySession _session;

    public GetProductByIdQueryHandler(IQuerySession session)
    {
        _session = session;
    }

    public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _session.LoadAsync<Product>(request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure<ProductResponse>(new Error(
                    "Product.NotFound",
                    $"The record with the id {request.Id} was not found"));
        }

        var response = new ProductResponse(
            product.Id,
            product.Barcode,
            product.Sku,
            product.Name,
            product.Description,
            product.Cost,
            product.Price,
            product.Earn,
            product.Image,
            product.UseInventory,
            product.Sizes,
            product.Colors,
            new ProductCategoryResponse(
                product.ProductCategory.Id,
                product.ProductCategory.Name,
                product.ProductCategory.Active),
            product.Taxes.Select(s =>
            new TaxResponse(
            s.Id,
            s.Name,
            s.ShortName,
            s.Percentage))
            .ToList(),
            product.Active);

        return Result.Success(response);
    }
}

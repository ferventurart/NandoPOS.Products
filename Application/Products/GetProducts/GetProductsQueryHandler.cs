using Aplication.ProductCategories.GetProductCategory;
using Application.Abstractions.Messaging;
using Application.Taxes.GetTax;
using Domain.Products;
using Domain.Shared;
using Marten;
using Microsoft.CodeAnalysis;

namespace Application.Products.GetProducts;

internal sealed class GetProductsQueryHandler
     : IQueryHandler<GetProductsQuery, List<ProductsResponse>>
{
    private readonly IQuerySession _session;

    public GetProductsQueryHandler(IQuerySession session)
    {
        _session = session;
    }

    public async Task<Result<List<ProductsResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
       IReadOnlyList<Product> products = await _session.Query<Product>().ToListAsync(cancellationToken);

       List<ProductsResponse> response = products.Select(
           product => new ProductsResponse(
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
           product.StockMin,
           product.StockMax,
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
           product.Active)).ToList();

       return Result.Success(response);
    }
}

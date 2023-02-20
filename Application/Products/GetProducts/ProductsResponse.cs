using Aplication.ProductCategories.GetProductCategory;
using Application.Taxes.GetTax;

namespace Application.Products.GetProducts;

public record ProductsResponse(
    Guid Id,
    string? Barcode,
    string? Sku,
    string Name,
    string? Description,
    decimal Cost,
    decimal Price,
    decimal Earn,
    string? Image,
    bool UseInventory,
    IReadOnlyCollection<string> Sizes,
    IReadOnlyCollection<string> Colors,
    ProductCategoryResponse ProductCategory,
    IReadOnlyCollection<TaxResponse> Taxes,
    bool Active);

namespace Presentation.Contracts.Products;

public record UpdateProductRequest(
    Guid Id,
    string? Barcode,
    string? Sku,
    string Name,
    string? Description,
    decimal Cost,
    decimal Price,
    string? Image,
    bool UseInventory,
    decimal? StockMin,
    decimal? StockMax,
    List<string> Sizes,
    List<string> Colors,
    List<Guid> Taxes,
    Guid ProductCategoryId);

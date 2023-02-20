namespace Presentation.Contracts.Products;

public record CreateProductRequest(
    string? Barcode,
    string? Sku,
    string Name,
    string? Description,
    decimal Cost,
    decimal Price,
    string? Image,
    bool UseInventory,
    List<string> Sizes,
    List<string> Colors,
    List<Guid> Taxes,
    Guid ProductCategoryId);

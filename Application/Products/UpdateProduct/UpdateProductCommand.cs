using Application.Abstractions.Messaging;

namespace Application.Products.UpdateProduct;

public record UpdateProductCommand(
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
    Guid ProductCategoryId,
    bool Active) : ICommand;

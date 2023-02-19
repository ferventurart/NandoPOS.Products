namespace Presentation.Contracts.ProductCategories;

public sealed record UpdateProductCategoryRequest(
    Guid Id,
    string Name,
    bool Active);

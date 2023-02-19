namespace Aplication.ProductCategories.GetProductCategory;

public sealed record ProductCategoryResponse(
    Guid Id,
    string Name,
    bool Active);

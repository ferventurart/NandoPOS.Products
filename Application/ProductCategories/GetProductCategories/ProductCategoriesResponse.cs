namespace Application.ProductCategories.GetProductCategories;

public sealed record ProductCategoriesResponse(
    Guid Id,
    string Name,
    bool Active);

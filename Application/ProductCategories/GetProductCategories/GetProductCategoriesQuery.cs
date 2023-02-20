using Application.Abstractions.Messaging;

namespace Application.ProductCategories.GetProductCategories;

public sealed record GetProductCategoriesQuery : IQuery<List<ProductCategoriesResponse>>;

using Application.Abstractions.Messaging;
using Application.Taxes.GetTaxes;

namespace Application.ProductCategories.GetProductCategories;

public sealed record GetProductCategoriesQuery : IQuery<List<ProductCategoriesResponse>>;

using Aplication.ProductCategories.GetProductCategory;
using Application.Abstractions.Messaging;

namespace Application.ProductCategories.GetProductCategory;

public sealed record GetProductCategoryByIdQuery(Guid Id) : IQuery<ProductCategoryResponse>;

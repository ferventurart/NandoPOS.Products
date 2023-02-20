using Application.Abstractions.Messaging;
using Application.ProductCategories.GetProductCategories;

namespace Application.Products.GetProducts;

public sealed record GetProductsQuery : IQuery<List<ProductsResponse>>;

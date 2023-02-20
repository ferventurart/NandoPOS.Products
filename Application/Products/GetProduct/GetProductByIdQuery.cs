using Aplication.ProductCategories.GetProductCategory;
using Application.Abstractions.Messaging;

namespace Application.Products.GetProduct;

public sealed record GetProductByIdQuery(Guid Id) : IQuery<ProductResponse>;

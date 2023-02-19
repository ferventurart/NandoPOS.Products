using Aplication.ProductCategories.GetProductCategory;
using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.ProductCategories.GetProductCategory;

internal sealed class GetProductCategoryByIdQueryHandler
    : IQueryHandler<GetProductCategoryByIdQuery, ProductCategoryResponse>
{
    private readonly IQuerySession _session;

    public GetProductCategoryByIdQueryHandler(IQuerySession session)
    {
        _session = session;
    }

    public async Task<Result<ProductCategoryResponse>> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var productCategory = await _session.LoadAsync<ProductCategory>(request.Id, cancellationToken);

        if (productCategory is null)
        {
            return Result.Failure<ProductCategoryResponse>(new Error(
                    "ProductCategory.NotFound",
                    $"The record with the id {request.Id} was not found"));
        }

        var response = new ProductCategoryResponse(productCategory.Id, productCategory.Name, productCategory.Active);

        return Result.Success(response);
    }
}

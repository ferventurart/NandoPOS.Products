using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.ProductCategories.GetProductCategories;

internal sealed class GetProductCategoriesQueryHandler
    : IQueryHandler<GetProductCategoriesQuery, List<ProductCategoriesResponse>>
{
    private readonly IQuerySession _session;

    public GetProductCategoriesQueryHandler(IQuerySession session)
    {
        _session = session;
    }

    public async Task<Result<List<ProductCategoriesResponse>>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<ProductCategoriesResponse> productCategories = await _session
            .Query<ProductCategory>()
            .Select(t => new ProductCategoriesResponse(
                t.Id,
                t.Name,
                t.Active))
            .ToListAsync(cancellationToken);

        return productCategories.ToList();
    }
}

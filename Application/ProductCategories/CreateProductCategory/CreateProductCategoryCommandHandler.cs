using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.ProductCategories.CreateProductCategory;

internal sealed class CreateProductCategoryCommandHandler : ICommandHandler<CreateProductCategoryCommand, Guid>
{
    private readonly IDocumentSession _session;

    public CreateProductCategoryCommandHandler(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<Result<Guid>> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        ProductCategory productCategory = new(request.Name);

        _session.Store(productCategory);

        await _session.SaveChangesAsync(cancellationToken);

        return productCategory.Id;
    }
}

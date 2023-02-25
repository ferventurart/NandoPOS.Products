using Application.Abstractions.EventBus;
using Application.Abstractions.Messaging;
using Domain.Products;
using Domain.Shared;
using Marten;

namespace Application.Products.CreateProduct;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IDocumentSession _session;
    private readonly IEventBus _eventBus;

    public CreateProductCommandHandler(IDocumentSession session, IEventBus eventBus)
    {
        _session = session;
        _eventBus = eventBus;
    }

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ProductCategory? productCategory = await _session.LoadAsync<ProductCategory>(request.ProductCategoryId, cancellationToken);

        if (productCategory is null)
        {
            return Result.Failure<Guid>(new Error(
                "Product.CategoryNotValid",
                $"The Product Category Id {request.ProductCategoryId} is not valid"));
        }

        Product product = new(
            request.Barcode,
            request.Sku,
            request.Name,
            request.Description,
            request.Cost,
            request.Price,
            request.UseInventory,
            request.StockMin,
            request.StockMax,
            productCategory);

        product.AddSizes(request.Sizes);
        product.AddColors(request.Colors);

        var taxes = await _session.Query<Tax>().ToListAsync(cancellationToken);
        product.AddTaxes(taxes, request.Taxes);

        _session.Store<Product>(product);

        await _session.SaveChangesAsync(cancellationToken);

        await _eventBus.PublishAsync(
                new ProductCreatedEvent
                {
                    Id = product.Id,
                    Barcode = product.Barcode,
                    Sku = product.Sku,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Image = product.Image,
                    StockMin = product.StockMin,
                    StockMax = product.StockMax,
                    Sizes = product.Sizes,
                    Colors = product.Colors,
                    ProductCategory = product.ProductCategory
                },
                cancellationToken);

        return product.Id;
    }
}

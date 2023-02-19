using Application.Abstractions.Messaging;

namespace Application.ProductCategories.CreateProductCategory;

public record CreateProductCategoryCommand(
    string Name) : ICommand<Guid>;

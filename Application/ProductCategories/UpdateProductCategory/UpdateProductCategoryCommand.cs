using Application.Abstractions.Messaging;

namespace Application.ProductCategories.UpdateProductCategory;

public record UpdateProductCategoryCommand(
    Guid Id,
    string Name,
    bool Active) : ICommand;

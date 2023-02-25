using Application.Abstractions.Messaging;

namespace Application.ProductCategories.DeleteProductCategory;

public record DeleteProductCategoryCommand(Guid Id) : ICommand;

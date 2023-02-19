using FluentValidation;

namespace Application.ProductCategories.CreateProductCategory;

public class CreateProductCategoryCommandValidator : AbstractValidator<CreateProductCategoryCommand>
{
    public CreateProductCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(60);
    }
}

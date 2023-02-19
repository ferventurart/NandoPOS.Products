using FluentValidation;

namespace Application.ProductCategories.UpdateProductCategory;

public class UpdateProductCategoryCommanValidator : AbstractValidator<UpdateProductCategoryCommand>
{
    public UpdateProductCategoryCommanValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Name).NotEmpty().MaximumLength(60);
    }
}

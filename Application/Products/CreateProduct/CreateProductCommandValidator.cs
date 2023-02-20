using FluentValidation;

namespace Application.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Barcode)
            .MaximumLength(20);

        RuleFor(x => x.Sku)
            .MaximumLength(35);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(200);

        RuleFor(x => x.Cost)
            .NotEmpty()
            .GreaterThan(0)
            .LessThan(x => x.Price);

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0)
            .GreaterThan(x => x.Cost);

        RuleForEach(x => x.Colors)
            .MaximumLength(6);

        RuleForEach(x => x.Sizes)
            .MaximumLength(3);
    }
}

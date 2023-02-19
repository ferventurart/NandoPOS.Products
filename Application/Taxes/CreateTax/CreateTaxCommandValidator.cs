using FluentValidation;

namespace Application.Taxes.CreateTax;

public class CreateTaxCommandValidator : AbstractValidator<CreateTaxCommand>
{
    public CreateTaxCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(60);

        RuleFor(x => x.ShortName).NotEmpty().MaximumLength(6);

        RuleFor(x => x.Percentage).GreaterThan(0);
    }
}

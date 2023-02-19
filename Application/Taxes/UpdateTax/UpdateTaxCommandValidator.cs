using FluentValidation;

namespace Application.Taxes.UpdateTax;

public class UpdateTaxCommandValidator : AbstractValidator<UpdateTaxCommand>
{
    public UpdateTaxCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.ShortName).NotEmpty();

        RuleFor(x => x.Percentage).GreaterThan(0);
    }
}

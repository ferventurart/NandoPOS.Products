using FluentValidation;

namespace Application.Taxes.DeleteTax;

public class DeleteTaxCommandValidator : AbstractValidator<DeleteTaxCommand>
{
    public DeleteTaxCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

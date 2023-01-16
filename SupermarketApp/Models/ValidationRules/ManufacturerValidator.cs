using FluentValidation;
using SupermarketApp.Core.Models;

namespace SupermarketApp.Core.Models.ValidationRules
{
    public class ManufacturerValidator : AbstractValidator<ManufacturerModel>
    {
        public ManufacturerValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name can't be empty")
                .MaximumLength(100).WithMessage("Your name is too long");
        }
    }
}

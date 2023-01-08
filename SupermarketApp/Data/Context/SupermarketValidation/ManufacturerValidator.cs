using FluentValidation;
using SupermarketApp.Models;

namespace SupermarketApp.Data.Context.SupermarketValidation
{
    public class ManufacturerValidator : AbstractValidator<Manufacturer>
    {
        public ManufacturerValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name can't be empty")
                .MaximumLength(100).WithMessage("Your name is too long");
        }
    }
}

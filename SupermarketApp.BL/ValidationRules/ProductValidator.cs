using FluentValidation;
using SupermarketApp.Data.Entities;

namespace SupermarketApp.Data.Context.SupermarketValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name can't be empty")
                .MaximumLength(100).WithMessage("Your name is too long");

            RuleFor(d => d.Description)
                .NotEmpty().WithMessage("Description can't be empty")
                .MaximumLength(1000).WithMessage("Your description is too long");
        }
    }
}

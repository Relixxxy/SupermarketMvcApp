using FluentValidation;
using SupermarketApp.Core.Models;

namespace SupermarketApp.Core.Models.ValidationRules
{
    public class DepartmentValidator : AbstractValidator<DepartmentModel>
    {
        public DepartmentValidator()
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

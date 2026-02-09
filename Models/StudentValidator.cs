using DemoApi.Models;
using FluentValidation;

namespace DemoApi.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.");

            RuleFor(s => s.Age)
                .InclusiveBetween(5, 100).WithMessage("Age must be between 5 and 100.");
        }
    }
}

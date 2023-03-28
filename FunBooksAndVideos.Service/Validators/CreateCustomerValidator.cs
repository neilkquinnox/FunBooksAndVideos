using FluentValidation;
using FunBooksAndVideos.Service.Resources;

namespace FunBooksAndVideos.Service.Validators
{
    public class CreateCustomerValidator : AbstractValidator<CustomerRequest>
    {
        public CreateCustomerValidator()
        {
            RuleFor(c => c.Phone).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email address is required.");
            RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage("A valid email address is required.");
        }
    }
}
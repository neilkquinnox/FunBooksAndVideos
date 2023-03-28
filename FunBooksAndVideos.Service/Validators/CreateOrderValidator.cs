using FluentValidation;
using FunBooksAndVideos.Service.Resources;

namespace FunBooksAndVideos.Service.Validators
{
    public class CreateOrderValidator : AbstractValidator<OrderRequest>
    {
        public CreateOrderValidator()
        {
            RuleFor(c => c.Customer_ID).NotEmpty();
        }
    }
}
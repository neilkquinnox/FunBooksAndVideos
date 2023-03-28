using FluentValidation;
using FunBooksAndVideos.Service.Resources;

namespace FunBooksAndVideos.Service.Validators
{
    public class CreateProductValidator : AbstractValidator<ProductRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(c => c.Barcode).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
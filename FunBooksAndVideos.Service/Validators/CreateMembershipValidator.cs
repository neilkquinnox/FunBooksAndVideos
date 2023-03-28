using FluentValidation;
using FunBooksAndVideos.Service.Resources;

namespace FunBooksAndVideos.Service.Validators
{
    public class CreateMembershipValidator : AbstractValidator<MembershipRequest>
    {
        public CreateMembershipValidator()
        {
            RuleFor(c => c.Price).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
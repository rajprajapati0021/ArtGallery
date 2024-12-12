using ArtGallery.RequestModels;
using FluentValidation;

namespace ArtGallery.Validators
{
    public class UserValidator : AbstractValidator<UserRequestModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull();
            RuleFor(x => x.LastName).NotEmpty().NotNull();
            RuleFor(x => x.Email).NotEmpty().NotNull();
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull();
            RuleFor(x => x.Age).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull().When(x => x.Id == 0 );
        }
    }
}

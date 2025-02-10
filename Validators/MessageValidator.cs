using ArtGallery.RequestModels;
using FluentValidation;

namespace ArtGallery.Validators
{
    public class MessageValidator : AbstractValidator<AddUpdateMessageRequestModel>
    {
        public MessageValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Text).NotEmpty().NotNull();
            RuleFor(x => x.Type).NotEmpty().NotNull();
            RuleFor(x => x.Time).NotEmpty().NotNull();
            RuleFor(x => x.SenderId).NotEmpty().NotNull();
            RuleFor(x => x.RecieverId).NotEmpty().NotNull();

        }
    }
}
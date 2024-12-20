using ArtGallery.RequestModels;
using FluentValidation;

namespace ArtGallery.Validators
{
    public class ProductValidator : AbstractValidator<AddUpdateProductRequestModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Price).NotEmpty().NotNull();
            RuleFor(x => x.Stock).NotEmpty().NotNull();

        }
    }
}
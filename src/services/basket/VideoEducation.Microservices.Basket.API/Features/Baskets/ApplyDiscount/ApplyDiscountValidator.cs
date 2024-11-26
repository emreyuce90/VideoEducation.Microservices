using FluentValidation;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.ApplyDiscount {
    public class ApplyDiscountValidator:AbstractValidator<ApplyDiscountBasketCommand> {
        public ApplyDiscountValidator()
        {
            RuleFor(x => x.coupon)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.discountRate)
                .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} have to be minimum 0 and greater");
        }
    }
}

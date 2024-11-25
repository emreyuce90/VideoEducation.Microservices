using FluentValidation;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.Create {
    public class CreateBasketCommandValidator :AbstractValidator<CreateBasketCommand>{
        public CreateBasketCommandValidator()
        {
            RuleFor(c => c.courseName)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty")
            .MaximumLength(100)
            .WithMessage("{PropertyName} cannot be greater than {PropertyValue} length character");


            RuleFor(c => c.coursePrice)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty")
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be a number equal to or greater than 0.");


            RuleFor(c => c.courseId)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty");
        }
    }
}

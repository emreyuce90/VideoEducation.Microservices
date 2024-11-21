using FluentValidation;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses.Update {
    public class CourseUpdateCommandValidator:AbstractValidator<CourseUpdateCommand> {
        public CourseUpdateCommandValidator()
        {
            RuleFor(c => c.Name)
           .NotEmpty()
           .WithMessage("{PropertyName} cannot be empty")
           .MaximumLength(100)
           .WithMessage("{PropertyName} cannot be greater than {PropertyValue} length character");


            RuleFor(c => c.Price)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Price cannot be empty")
            .Must(price => int.TryParse(price.ToString(), out var value) && value >= 0)
            .WithMessage("{PropertyName} must be a number equal to or greater than 0.");


            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be null")
                .Length(5, 1000)
                .WithMessage("{PropertyName} must be between {From} and {To} lengt");

            RuleFor(c => c.CategoryId)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty");
        }
    }
}

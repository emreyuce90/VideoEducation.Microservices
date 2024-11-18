using FluentValidation;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.Create {
    public class CreateCategoryCommandValidator :AbstractValidator<CreateCategoryCommand>{
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name alanı boş geçilemez")
                .Length(3, 100)
                .WithMessage("Name alanı 3 ila 100 karakter arasında olabilir");
                
        }
    }
}

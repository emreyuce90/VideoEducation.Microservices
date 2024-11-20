using FluentValidation;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Dtos;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.Update {
    public class CategoryUpdateDtoValidator:AbstractValidator<CategoryUpdateDto> {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name field cannot be empty");
        }
    }
}

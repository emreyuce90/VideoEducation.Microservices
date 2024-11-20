using MediatR;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Dtos;
using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.Update {
    public class CategoryUpdateCommandRequest :IRequestServiceResult<CategoryUpdateDto>{
        public CategoryUpdateDto CategoryUpdateDto { get; set; } = default!;
    }
}

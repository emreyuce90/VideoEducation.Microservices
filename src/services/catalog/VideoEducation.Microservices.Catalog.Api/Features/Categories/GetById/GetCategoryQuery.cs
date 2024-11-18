using MediatR;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Dtos;
using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.GetById {
    public record GetCategoryQuery (Guid Id):IRequestServiceResult<CategoryDto>;
        
}

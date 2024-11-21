using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses.Delete {
    public record DeleteCategoryCommand(Guid Id):IRequestServiceResult<Guid>;
}

using VideoEducation.Microservices.Catalog.Api.Features.Courses.Dtos;
using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses.GetByUserId {
    public record GetCourseByUserIdQuery(Guid UserId):IRequestServiceResult<List<CourseDto>>;
}

using VideoEducation.Microservices.Catalog.Api.Features.Courses.Dtos;
using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses.GetById
{
    public record GetCourseByIdQuery (Guid Id):IRequestServiceResult<CourseDto>;
}

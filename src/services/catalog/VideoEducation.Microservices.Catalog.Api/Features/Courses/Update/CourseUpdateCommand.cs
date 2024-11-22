using VideoEducation.Microservices.Catalog.Api.Features.Courses.Dtos;
using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses.Update
{
    public record CourseUpdateCommand (Guid Id,string Name, string Description, string? PicureUrl, decimal Price, Guid CategoryId):IRequestServiceResult<CourseDto>;
}

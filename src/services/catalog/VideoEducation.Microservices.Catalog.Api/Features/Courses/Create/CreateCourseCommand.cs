using MediatR;
using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses.Create {
    public record CreateCourseCommand(string Name, string Description, string? PicureUrl, decimal Price,Guid CategoryId) : IRequestServiceResult<Guid>;
}


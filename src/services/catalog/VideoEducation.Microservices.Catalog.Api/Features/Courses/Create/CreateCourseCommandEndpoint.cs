using MediatR;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Create;
using VideoEducation.Microservices.Shared.Extensions;
using VideoEducation.Microservices.Shared.Filters;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses.Create {
    public static class CreateCourseCommandEndpoint {
        public static RouteGroupBuilder CreateCourseGroupItem(this RouteGroupBuilder group) {
            group.MapPost("/", async (CreateCourseCommand command, IMediator mediator) => {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            }).AddEndpointFilter<ValidationFilter<CreateCourseCommand>>();

            return group;
        }
    }
}

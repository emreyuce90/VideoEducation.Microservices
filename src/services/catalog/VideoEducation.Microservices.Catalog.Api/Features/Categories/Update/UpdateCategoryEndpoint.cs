using MediatR;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Create;
using VideoEducation.Microservices.Shared.Extensions;
using VideoEducation.Microservices.Shared.Filters;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.Update {
    public static class UpdateCategoryEndpoint {
        public static RouteGroupBuilder UpdateCategoryGroupItem(this RouteGroupBuilder group) {
            group.MapPut("/", async (CategoryUpdateCommandRequest command, IMediator mediator) => {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            }).AddEndpointFilter<ValidationFilter<CategoryUpdateDtoValidator>>().MapToApiVersion(1,0);

            return group;
        }
    }
}

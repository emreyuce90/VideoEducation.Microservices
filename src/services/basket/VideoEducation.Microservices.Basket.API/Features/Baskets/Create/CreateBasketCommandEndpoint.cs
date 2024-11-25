using MediatR;
using VideoEducation.Microservices.Shared.Extensions;
using VideoEducation.Microservices.Shared.Filters;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.Create {
    public static class CreateBasketCommandEndpoint {
        public static RouteGroupBuilder CreateBasketGroupItem(this RouteGroupBuilder group) {
            group.MapPost("/item", async (CreateBasketCommand command, IMediator mediator) => {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            }).AddEndpointFilter<ValidationFilter<CreateBasketCommand>>().MapToApiVersion(1, 0);

            return group;
        }
    }
}

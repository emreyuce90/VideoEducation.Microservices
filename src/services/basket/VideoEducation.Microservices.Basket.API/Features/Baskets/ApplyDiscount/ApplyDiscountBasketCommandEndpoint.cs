using MediatR;
using VideoEducation.Microservices.Shared.Extensions;
using VideoEducation.Microservices.Shared.Filters;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.ApplyDiscount {
    public static class ApplyDiscountBasketCommandEndpoint
    {
        public static RouteGroupBuilder ApplyDiscountBasketGroupItem(this RouteGroupBuilder group) {
            group.MapPut("/apply-discount", async (ApplyDiscountBasketCommand command,IMediator mediator) => {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            }).AddEndpointFilter<ValidationFilter<ApplyDiscountBasketCommand>>().MapToApiVersion(1, 0);

            return group;
        }
    }
}

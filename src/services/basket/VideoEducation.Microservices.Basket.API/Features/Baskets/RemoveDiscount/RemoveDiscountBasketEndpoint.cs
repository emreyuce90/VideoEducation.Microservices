using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using VideoEducation.Microservices.Shared;
using VideoEducation.Microservices.Shared.Extensions;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.RemoveDiscount {
    public record RemoveDiscountBasketCommand :IRequestServiceResult;

    public class RemoveDiscountBasketCommandHandler (BasketService basketService): IRequestHandler<RemoveDiscountBasketCommand, ServiceResult> {
        public async Task<ServiceResult> Handle(RemoveDiscountBasketCommand request, CancellationToken cancellationToken) {

            var basketAsJson = await basketService.GetBasketCache(cancellationToken);
            
            if (basketAsJson is null) {
            return ServiceResult.Error("Basket not found",HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Basket>(basketAsJson);
            if (!basket!.Items.Any(x => x.DiscountedPrice.HasValue)) {
                return ServiceResult.Error("There is no discount on this basket to remove", HttpStatusCode.NotFound);
            }

            basket.RemoveDiscount();
            await basketService.CreateBasketCache(basket,cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }

    public static class RemoveDiscountBasketEndpoint {

        public static RouteGroupBuilder RemoveDiscountFromBasketGroupItem(this RouteGroupBuilder group) {
            group.MapPut("/remove-discount", async ([FromServices] IMediator mediator) => {
                var result = await mediator.Send(new RemoveDiscountBasketCommand());
                return result.ToGenericResult();
            }).MapToApiVersion(1, 0);

            return group;
        }
    }


}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;
using VideoEducation.Microservices.Basket.API.Features.Baskets.Create;
using VideoEducation.Microservices.Basket.API.Helpers;
using VideoEducation.Microservices.Shared;
using VideoEducation.Microservices.Shared.Extensions;
using VideoEducation.Microservices.Shared.Filters;
using VideoEducation.Microservices.Shared.Services;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.RemoveDiscount {
    public record RemoveDiscountBasketCommand :IRequestServiceResult;

    public class RemoveDiscountBasketCommandHandler (IIdentityService identityService,IDistributedCache distributedCache): IRequestHandler<RemoveDiscountBasketCommand, ServiceResult> {
        public async Task<ServiceResult> Handle(RemoveDiscountBasketCommand request, CancellationToken cancellationToken) {
            var cacheKey = CacheKeyHelper.GetCacheKey(identityService.UserId);
            var basketAsJson = await distributedCache.GetStringAsync(cacheKey,cancellationToken);
            
            if (basketAsJson is null) {
            return ServiceResult.Error("Basket not found",HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Basket>(basketAsJson);
            if (!basket!.Items.Any(x => x.DiscountedPrice.HasValue)) {
                return ServiceResult.Error("There is no discount on this basket to remove", HttpStatusCode.NotFound);
            }

            basket.RemoveDiscount();
            basketAsJson = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsJson,cancellationToken);  
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

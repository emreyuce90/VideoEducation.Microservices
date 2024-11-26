using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;
using VideoEducation.Microservices.Shared;
using VideoEducation.Microservices.Shared.Services;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.ApplyDiscount {
    public class ApplyDiscountBasketCommandHandler(BasketService basketService) : IRequestHandler<ApplyDiscountBasketCommand, ServiceResult> {
        public async Task<ServiceResult> Handle(ApplyDiscountBasketCommand request, CancellationToken cancellationToken) {
           
            var jsonBasket = await basketService.GetBasketCache(cancellationToken);

            if (jsonBasket == null) {
                return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);
            }


            var basket = JsonSerializer.Deserialize<Basket>(jsonBasket);

            if (!basket!.Items.Any()) {
                return ServiceResult.Error("Basket item not found", HttpStatusCode.NotFound);

            }

            if (basket.Items.Any(x=>x.DiscountedPrice.HasValue)) {
                return ServiceResult.Error("Your basket already discounted", HttpStatusCode.NotFound);

            }

            basket!.ApplyNewDiscount(request.coupon, request.discountRate);
           
            await basketService.CreateBasketCache(basket,cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}

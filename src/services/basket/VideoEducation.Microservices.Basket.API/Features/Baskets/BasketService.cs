
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using VideoEducation.Microservices.Basket.API.Helpers;
using VideoEducation.Microservices.Shared.Services;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets {
    public class BasketService(IIdentityService identityService, IDistributedCache distributedCache) {

        private string GetCacheKey (){
            var userId = identityService.UserId;
            return CacheKeyHelper.GetCacheKey(userId);
        }

        public async Task CreateBasketCache(Basket basket,CancellationToken cancellationToken) {
           var serializedBasket = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(GetCacheKey(), serializedBasket, cancellationToken);
        }

        public async Task<string?> GetBasketCache(CancellationToken cancellationToken){

            return await distributedCache.GetStringAsync(GetCacheKey(), cancellationToken);
        }
    }
}

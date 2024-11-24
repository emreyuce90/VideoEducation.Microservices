using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using VideoEducation.Microservices.Basket.API.Constants;
using VideoEducation.Microservices.Basket.API.Features.Baskets.Dtos;
using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.Create {
    public class CreateBasketCommandHandler(IDistributedCache distributedCache) : IRequestHandler<CreateBasketCommand, ServiceResult> {
        public async Task<ServiceResult> Handle(CreateBasketCommand request, CancellationToken cancellationToken) {
            var userId = Guid.NewGuid(); //xDWPgq7B4oX1cpiXNl798hD5Xp
            var cachedKey = String.Format(CacheKey.GetCacheKey, userId.ToString()); //Basket:xDWPgq7B4oX1cpiXNl798hD5Xp
            var basketAsString = await distributedCache.GetStringAsync(cachedKey, cancellationToken);

            BasketDto? currentBasket;

            BasketItemDto newItem = new(request.courseId, request.courseName, request.coursePrice, request.discountedPrice, request.ImageUrl);

            if (String.IsNullOrEmpty(basketAsString)) {
                currentBasket = new BasketDto(userId, [newItem]);
                return await WriteDataToCacheAndReturnSuccess(currentBasket, cachedKey, cancellationToken);
            }
            currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);
            var isSameCourse = currentBasket!.items.FirstOrDefault(c => c.courseId == request.courseId);
            if (isSameCourse is not null) {

                currentBasket.items.Remove(isSameCourse);
            }
            currentBasket.items.Add(newItem);

            return await WriteDataToCacheAndReturnSuccess(currentBasket, cachedKey, cancellationToken);

        }
        private async Task<ServiceResult> WriteDataToCacheAndReturnSuccess(BasketDto basketDto, string cachedKey, CancellationToken cancellationToken) {
            var serializedData = JsonSerializer.Serialize<BasketDto>(basketDto);
            await distributedCache.SetStringAsync(cachedKey, serializedData, cancellationToken)
             return ServiceResult.SuccessAsNoContent();
        }
        //tekrar eden kodlar ortak private metoda alınır
        //programlamada fast fail esastır bir an önce cevabı döndürmeliyiz
    }


}

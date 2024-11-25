using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using VideoEducation.Microservices.Basket.API.Constants;
using VideoEducation.Microservices.Basket.API.Features.Baskets.Dtos;
using VideoEducation.Microservices.Shared;
using VideoEducation.Microservices.Shared.Services;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.Create {
    public class CreateBasketCommandHandler(IDistributedCache distributedCache,IIdentityService identityService) : IRequestHandler<CreateBasketCommand, ServiceResult> {
        public async Task<ServiceResult> Handle(CreateBasketCommand request, CancellationToken cancellationToken) {
            var userId = identityService.UserId; //xDWPgq7B4oX1cpiXNl798hD5Xp
            var cachedKey = String.Format(CacheKey.GetCacheKey, userId.ToString()); //Basket:xDWPgq7B4oX1cpiXNl798hD5Xp
            var basketAsString = await distributedCache.GetStringAsync(cachedKey, cancellationToken);

            BasketDto? currentBasket;

            BasketItemDto newItem = new(request.courseId, request.courseName, request.coursePrice, null, request.ImageUrl);

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
            await distributedCache.SetStringAsync(cachedKey, serializedData, cancellationToken);
             return ServiceResult.SuccessAsNoContent();
        }
        //tekrar eden kodlar ortak private metoda alınır
        //programlamada fast fail esastır bir an önce cevabı döndürmeliyiz
    }


}

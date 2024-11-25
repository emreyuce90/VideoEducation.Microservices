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

            Basket currentBasket;

            BasketItem newItem = new(request.courseId, request.courseName, request.coursePrice, null, request.ImageUrl);

            if (String.IsNullOrEmpty(basketAsString)) {
                currentBasket = new Basket(userId, [newItem]);
                return await WriteDataToCacheAndReturnSuccess(currentBasket, cachedKey, cancellationToken);
            }
            currentBasket = JsonSerializer.Deserialize<Basket>(basketAsString);
            var isSameCourse = currentBasket!.Items.FirstOrDefault(c => c.Id == request.courseId);
            if (isSameCourse is not null) {

                currentBasket.Items.Remove(isSameCourse);
            }
            currentBasket.Items.Add(newItem);

            return await WriteDataToCacheAndReturnSuccess(currentBasket, cachedKey, cancellationToken);

        }
        private async Task<ServiceResult> WriteDataToCacheAndReturnSuccess(Basket basket, string cachedKey, CancellationToken cancellationToken) {
            var serializedData = JsonSerializer.Serialize<Basket>(basket);
            await distributedCache.SetStringAsync(cachedKey, serializedData, cancellationToken);
             return ServiceResult.SuccessAsNoContent();
        }
        //tekrar eden kodlar ortak private metoda alınır
        //programlamada fast fail esastır bir an önce cevabı döndürmeliyiz
    }


}

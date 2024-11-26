using MediatR;
using System.Text.Json;
using VideoEducation.Microservices.Shared;
using VideoEducation.Microservices.Shared.Services;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.Create {
    public class CreateBasketCommandHandler(IIdentityService identityService,BasketService basketService) : IRequestHandler<CreateBasketCommand, ServiceResult> {
        public async Task<ServiceResult> Handle(CreateBasketCommand request, CancellationToken cancellationToken) {

            var basketAsJson = await basketService.GetBasketCache(cancellationToken);

            Basket currentBasket;

            BasketItem newItem = new(request.courseId, request.courseName, request.coursePrice, null, request.ImageUrl);

            if (String.IsNullOrEmpty(basketAsJson)) {
                currentBasket = new Basket(identityService.UserId, [newItem]);
                await basketService.CreateBasketCache(currentBasket,cancellationToken);
                return ServiceResult.SuccessAsNoContent();

            }
            currentBasket = JsonSerializer.Deserialize<Basket>(basketAsJson);
            var isSameCourse = currentBasket!.Items.FirstOrDefault(c => c.Id == request.courseId);
            if (isSameCourse is not null) {

                currentBasket.Items.Remove(isSameCourse);
            }
            currentBasket.Items.Add(newItem);
            currentBasket.ApplyAvaliableDiscount();
            await basketService.CreateBasketCache(currentBasket,cancellationToken);
            return ServiceResult.SuccessAsNoContent();

        }
    }


}

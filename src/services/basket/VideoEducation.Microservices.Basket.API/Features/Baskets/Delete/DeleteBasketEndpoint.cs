using MediatR;
using System.Net;
using System.Text.Json;
using VideoEducation.Microservices.Shared;
using VideoEducation.Microservices.Shared.Extensions;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.Delete {

    public record DeleteBasketCommand(Guid courseId):IRequestServiceResult;

    public class DeleteBasketCommandHandler(BasketService basketService) : IRequestHandler<DeleteBasketCommand, ServiceResult> {
        public async Task<ServiceResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken) {

            var cachedData = await basketService.GetBasketCache(cancellationToken);
            if (cachedData == null) {
                return ServiceResult.Error("There is no such as userId in our database",HttpStatusCode.NotFound);
            }

            var convertedData = JsonSerializer.Deserialize<Basket>(cachedData);
            var deletedItem = convertedData?.Items.FirstOrDefault(b=>b.Id == request.courseId);
            if (deletedItem is null) {
                return ServiceResult.Error("There is no item in your basket",$"The courseId ({request.courseId}) not exist in your basket",HttpStatusCode.NotFound);
            }

            //if (convertedData!.Items.Count() <= 1) { 
            //    await distributedCache.RemoveAsync(cacheKey, cancellationToken);
            //    return ServiceResult.SuccessAsNoContent();
            //}

            convertedData!.Items.Remove(deletedItem);
            await basketService.CreateBasketCache(convertedData,cancellationToken);
            return ServiceResult.SuccessAsNoContent();

        }
    }

    public static class DeleteBasketEndpoint {
        public static RouteGroupBuilder DeleteBasketGroupItem(this RouteGroupBuilder group) {
            group.MapDelete("/item/{courseId:guid}", async (Guid courseId, IMediator mediator) => {
                var result = await mediator.Send(new DeleteBasketCommand(courseId));
                return result.ToGenericResult();
            }).MapToApiVersion(1, 0);

            return group;
        }
    }
}

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;
using VideoEducation.Microservices.Basket.API.Constants;
using VideoEducation.Microservices.Basket.API.Features.Baskets.Dtos;
using VideoEducation.Microservices.Basket.API.Helpers;
using VideoEducation.Microservices.Shared;
using VideoEducation.Microservices.Shared.Extensions;
using VideoEducation.Microservices.Shared.Filters;
using VideoEducation.Microservices.Shared.Services;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.GetAll {
    public class GetBasketQueryHandler (IDistributedCache distributedCache,IIdentityService identityService,IMapper mapper): IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>> {
        public async  Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken) {
           var cacheKey = CacheKeyHelper.GetCacheKey(identityService.UserId);
            var basket = await distributedCache.GetStringAsync(cacheKey,cancellationToken);
            if(basket is null) {
                return ServiceResult<BasketDto>.Error("Basket not found",HttpStatusCode.NotFound);
            }
            var currentBasket = JsonSerializer.Deserialize<Basket>(basket);
           var mappedValue = mapper.Map<BasketDto>(currentBasket);
            return ServiceResult<BasketDto>.SuccessAsOk(mappedValue);
        }
    }


    public static class GetBasketQueryEndpoint {
        public static RouteGroupBuilder GetBasketGroupItem(this RouteGroupBuilder group) {
            group.MapGet("/user", async ([FromServices]IMediator mediator) => {
                var result = await mediator.Send(new GetBasketQuery());
                return result.ToGenericResult();
            }).MapToApiVersion(1, 0);

            return group;
        }
    }
}

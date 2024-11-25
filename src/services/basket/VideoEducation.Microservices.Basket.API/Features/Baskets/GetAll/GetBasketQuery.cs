using VideoEducation.Microservices.Basket.API.Features.Baskets.Dtos;
using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.GetAll {
    public record GetBasketQuery():IRequestServiceResult<BasketDto>;
}

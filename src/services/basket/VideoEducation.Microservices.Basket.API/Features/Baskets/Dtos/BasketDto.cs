namespace VideoEducation.Microservices.Basket.API.Features.Baskets.Dtos {
    public record BasketDto (Guid userId,List<BasketItemDto> items);
}

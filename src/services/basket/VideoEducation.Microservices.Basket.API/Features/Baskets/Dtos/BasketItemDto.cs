namespace VideoEducation.Microservices.Basket.API.Features.Baskets.Dtos {
    public record BasketItemDto(Guid courseId, string courseName, decimal coursePrice, decimal? discountedPrice, string? ImageUrl);
        
}

using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.Create {
    public record CreateBasketCommand(Guid courseId, string courseName, decimal coursePrice,string? ImageUrl) : IRequestServiceResult;
}

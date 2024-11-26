using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.ApplyDiscount {
    public record ApplyDiscountBasketCommand (string coupon,float discountRate):IRequestServiceResult;
}

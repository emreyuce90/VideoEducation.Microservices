using System.Text.Json.Serialization;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.Dtos {
    //içerisine userId ve Items verildiğinde bunları propertylerine set eder
    //nesne örneği alındığında ıtemsları newler 
    public record BasketDto {

        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }


        [JsonIgnore] public bool IsDiscountApply => DiscountRate > 0 && !String.IsNullOrEmpty(Coupon);
        //Total price without disc
        public decimal TotalPrice => Items.Sum(bi => bi.Price);
        public decimal? TotalPriceWDiscount => !IsDiscountApply ? null : Items.Sum(x => x.DiscountedPrice);



        [JsonIgnore]public Guid UserId { get; init; }
        public List<BasketItemDto> Items { get; set; } = new();

        public BasketDto(List<BasketItemDto> items)
        {
            Items = items;
        }

        public BasketDto()
        {
        }


    }
}

using System.Text.Json.Serialization;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets.Dtos {
    //içerisine userId ve Items verildiğinde bunları propertylerine set eder
    //nesne örneği alındığında ıtemsları newler 
    public record BasketDto {
        [JsonIgnore]public Guid UserId { get; init; }
        public List<BasketItemDto> Items { get; set; }

        public BasketDto(Guid userId,List<BasketItemDto> items)
        {
            UserId = userId;
            Items = items;
        }

        public BasketDto()
        {
            Items = new List<BasketItemDto>();

        }


    }
}

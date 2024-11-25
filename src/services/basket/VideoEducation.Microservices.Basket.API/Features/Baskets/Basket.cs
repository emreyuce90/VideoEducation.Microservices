namespace VideoEducation.Microservices.Basket.API.Features.Baskets {
    public class Basket {
        public Guid UserId { get; set; }
        public List<BasketItem> Items { get; set; } = new();

        public Basket(Guid userId,List<BasketItem> items)
        {
            UserId = userId;
            Items = items;
        }
    }
}

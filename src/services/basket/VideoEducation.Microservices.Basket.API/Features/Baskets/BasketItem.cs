namespace VideoEducation.Microservices.Basket.API.Features.Baskets {
    public class BasketItem {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public string? ImageUrl { get; set; }

        public BasketItem(Guid id,string name,decimal price,decimal? discountedPrice,string? imageUrl)
        {
            Id = id;
            Name = name;
            Price = price;
            DiscountedPrice = discountedPrice;
            ImageUrl = imageUrl;
        }


    }
}

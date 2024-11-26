using System.Reflection;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets {
    //Anemic classtan rich domaine dönen class
    public class Basket {
        public Guid UserId { get; set; }
        public List<BasketItem> Items { get; set; } = new();

        public Basket(Guid userId, List<BasketItem> items) {
            UserId = userId;
            Items = items;
        }

        public Basket() {

        }

        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }

      


        //Eğer indirim oranı 0 dan büyükse ve kupon doluysa true döner
        public bool IsDiscountApply => DiscountRate > 0 && !String.IsNullOrEmpty(Coupon);
        //Total price without disc
        public decimal TotalPrice => Items.Sum(bi => bi.Price);
        public decimal? TotalPriceWDiscount => !IsDiscountApply ? null : Items.Sum(x => x.DiscountedPrice);

        public void ApplyNewDiscount(string coupone, float discountRate) {
            DiscountRate = discountRate;
            Coupon = coupone;

            foreach (var basketItem in Items) {

                basketItem.DiscountedPrice = basketItem.Price * (decimal)(1 - discountRate);
            }
        }

        public void ApplyAvaliableDiscount() {
            foreach (var basketItem in Items) {
                basketItem.DiscountedPrice = basketItem.Price * (decimal)(1 - DiscountRate!);
            }
        }

        public void RemoveDiscount() {
            DiscountRate = null;
            Coupon = null;
            foreach (var basketItem in Items) { 
                basketItem.DiscountedPrice=null;
            }
        }
        
    }
}

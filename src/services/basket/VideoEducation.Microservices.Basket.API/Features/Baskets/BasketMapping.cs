using AutoMapper;
using VideoEducation.Microservices.Basket.API.Features.Baskets.Dtos;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets {
    public class BasketMapping :Profile{
        public BasketMapping()
        {
            CreateMap<Basket,BasketDto>().ReverseMap();
            CreateMap<BasketItem,BasketItemDto>().ReverseMap();
        }
    }
}

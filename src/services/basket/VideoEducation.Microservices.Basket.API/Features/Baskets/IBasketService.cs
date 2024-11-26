namespace VideoEducation.Microservices.Basket.API.Features.Baskets {
    public interface IBasketService {
        Task<string?> GetBasketCache(CancellationToken cancellationToken);
        Task CreateBasketCache(Basket basket,CancellationToken cancellationToken);
    }
}

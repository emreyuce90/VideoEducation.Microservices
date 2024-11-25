using Asp.Versioning;
using Asp.Versioning.Builder;
using System.Runtime.CompilerServices;
using VideoEducation.Microservices.Basket.API.Features.Baskets.Create;
using VideoEducation.Microservices.Basket.API.Features.Baskets.Delete;

namespace VideoEducation.Microservices.Basket.API.Features.Baskets {
    public static class BasketEndpointExt {
        public static void AddBasketGroupEndpointExt(this WebApplication application, ApiVersionSet apiVersionSet) {
            application.MapGroup(prefix: "api/v{version:apiVersion}/baskets")
                .WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .CreateBasketGroupItem()
                .DeleteBasketGroupItem();
        }
    }
}

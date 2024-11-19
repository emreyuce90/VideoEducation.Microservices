using VideoEducation.Microservices.Catalog.Api.Features.Categories.Create;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Delete;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.GetAll;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.GetById;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories {
    public static class CategoryEndpointExt {
        public static void AddCategoryGroupEndpointExt(this WebApplication application) {
            //Category ekleme apinin gruba dahil edilmesi
            application.MapGroup(prefix: "api/categories")
                .CreateCategoryGroupItem()
                .GetAllCategoryGroupItem()
                .GetCategoryGroupItem()
                .DeleteCategoryGroupItem();
        }
    }
}

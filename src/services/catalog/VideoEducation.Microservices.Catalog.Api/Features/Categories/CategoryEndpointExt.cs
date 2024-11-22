using Asp.Versioning.Builder;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Create;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Delete;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.GetAll;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.GetById;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Update;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories {
    public static class CourseEndpointExt {
        //apiVersion set daha önce extensionlarımızda yazdığımız apiVersiyonlarını içerir örneğin 1.0 2.0
        //version:apiVersion kodu ile bu generic constraintler eşleştirilir
        public static void AddCategoryGroupEndpointExt(this WebApplication application,ApiVersionSet apiVersionSet) {
            //Category ekleme apinin gruba dahil edilmesi
            application.MapGroup(prefix: "api/v{version:apiVersion}/categories")
                .WithApiVersionSet(apiVersionSet)
                .WithTags("Categories")
                .CreateCategoryGroupItem()
                .GetAllCategoryGroupItem()
                .GetCategoryGroupItem()
                .DeleteCategoryGroupItem()
                .UpdateCategoryGroupItem();
        }
    }
}

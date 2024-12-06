using VideoEducation.Microservices.Catalog.Api.Features.Categories.Repositories;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.Ext {
    public static class CategoryIoC {
        public static IServiceCollection AddCategoriesExt(this IServiceCollection services) { 
        services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}

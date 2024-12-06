
namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.Repositories {
    public interface ICategoryRepository {
        Task AddCategory(Category category,CancellationToken cancellationToken);
        Task<bool> IsCategoryExist(string name,CancellationToken cancellationToken);
    }
}
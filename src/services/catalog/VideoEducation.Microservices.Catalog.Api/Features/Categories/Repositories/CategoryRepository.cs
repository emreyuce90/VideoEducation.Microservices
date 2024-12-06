using Microsoft.EntityFrameworkCore;
using VideoEducation.Microservices.Catalog.Api.Repositories;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.Repositories {
    public class CategoryRepository(AppDbContext dbContext) : ICategoryRepository {
        public async Task<bool> IsCategoryExist(string name ,CancellationToken cancellationToken) {
            return await dbContext.Categories.AnyAsync(c => c.Name.Trim().ToLower() == name.Trim().ToLower(),cancellationToken);
        }

        public async Task AddCategory(Category category,CancellationToken cancellationToken) {
            await dbContext.Categories.AddAsync(category,cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

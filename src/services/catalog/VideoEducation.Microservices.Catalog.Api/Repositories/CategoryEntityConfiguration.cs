using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection.Emit;
using VideoEducation.Microservices.Catalog.Api.Features.Categories;

namespace VideoEducation.Microservices.Catalog.Api.Repositories {
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category> {
        public void Configure(EntityTypeBuilder<Category> builder) {
            builder.ToCollection("categories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasMaxLength(100);
            //sql tarafında navigation propertyler otomatik algılanır ve tabloya yansıtılmaz fakat mongo db tarafında bizim bu propertyleri ignore ettiğimizi ayrıca bildirmemiz gerekir
            builder.Ignore(x => x.Courses);
        }
    }
}

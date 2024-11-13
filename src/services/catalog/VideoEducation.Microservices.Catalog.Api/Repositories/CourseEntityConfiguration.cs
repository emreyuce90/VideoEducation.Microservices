using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection.Emit;
using VideoEducation.Microservices.Catalog.Api.Features.Courses;

namespace VideoEducation.Microservices.Catalog.Api.Repositories {
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course> {
        public void Configure(EntityTypeBuilder<Course> builder) {
            //Collection NoSql veritabanlarında Tablo ismine karşılık gelir
            //Sütun field Satır document olarak adlandırılır
            builder.ToCollection("courses");
            //Course tablosunda Id alanı unique tir.
            builder.HasKey(x => x.Id);
            //Guid değerleri kendimiz vereceğimiz için mongo tarafında buna değer atanmasını engelledik
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasElementName("name").HasMaxLength(100);
            builder.Property(x => x.Description).HasElementName("description").HasMaxLength(1000);
            builder.Property(x => x.CategoryId).HasElementName("categoryId");
            builder.Property(x => x.CreatedDate).HasElementName("createdDate");
            builder.Property(x => x.Picture).HasElementName("picture");
            builder.Property(x => x.Price).HasElementName("price");
            builder.Property(x => x.Feature).HasElementName("feature");
            builder.Property(x => x.UserId).HasElementName("userId");
            //sql tarafında navigation propertyler otomatik algılanır ve tabloya yansıtılmaz fakat mongo db tarafında bizim bu propertyleri ignore ettiğimizi ayrıca bildirmemiz gerekir
            builder.Ignore(x => x.Category);

            //feature entitysi kendisine ait idsi olmadığı için bir owned entitydir

            builder.OwnsOne<Feature>(c => c.Feature, f => {
                f.HasElementName("feature");
                f.Property(x => x.EducatorFullName).HasElementName("educatorFullName").HasMaxLength(100);
                f.Property(x => x.Duration).HasElementName("duration");
                f.Property(x => x.Rating).HasElementName("rating");
            });
        }
    }
}

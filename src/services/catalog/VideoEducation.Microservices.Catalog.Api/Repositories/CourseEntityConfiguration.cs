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
            builder.Property(x => x.PictureUrl).HasElementName("pictureUrl");
            builder.Property(x => x.Price).HasElementName("price");
            builder.Property(x => x.UserId).HasElementName("userId");
            //sql tarafında navigation propertyler otomatik algılanır ve tabloya yansıtılmaz fakat mongo db tarafında bizim bu propertyleri ignore ettiğimizi ayrıca bildirmemiz gerekir
            builder.Ignore(x => x.Category);

            

          
        }
    }
}

using VideoEducation.Microservices.Catalog.Api.Features.Categories;
using VideoEducation.Microservices.Catalog.Api.Repositories;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses {
    public class Course :BaseEntity{
        //Nullable ifadelerde muhakkak değer atanması gerekiyorsa default! , null geçilebilecekse ? konulur
        //Efcore açısından da anlam taşır
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        //string zaten nullable bir değerdir burada soru işareti koyma amacımız compile a bu alanı null değer geleceğini bildirmemizdir
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public Guid UserId { get; set; }
        //DateTime objesi zone kısmını tutmaz o yüzden global uygulama geliştiriyorsak DateTimeOffset objesiyle çalışmalıyız
        public DateTime CreatedDate { get; set; }
        
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = default!;

        public Feature Feature { get; set; } = default!;
    }
}

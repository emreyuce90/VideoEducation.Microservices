using VideoEducation.Microservices.Catalog.Api.Features.Courses;
using VideoEducation.Microservices.Catalog.Api.Repositories;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories {
    public class Category :BaseEntity{
        //mutlaka doldurulması gereken alanlar default! ile işaretlenmelidir
        // Bu ifade ile bu özelliğin başlangıçta null olduğu fakat erişildiğinde muhakkak değer atanması gerektiği garanti altına alınmış olur
        public string Name { get; set; } = default!;

        public List<Course>? Courses { get; set; }
    }
}

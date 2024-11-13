using VideoEducation.Microservices.Catalog.Api.Repositories;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses
{
    //MongoDb kullandığımız için ekstradan Id kolonuna gerek yok,obje direkt olarak Course içerisinde obje olarak tutulacak
    public class Feature
    {
        public int Duration { get; set; }
        public float Rating { get; set; }
        public string EducateorFullName { get; set; } = default!;
    }
}

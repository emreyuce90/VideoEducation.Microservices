using MongoDB.Bson.Serialization.Attributes;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses.Dtos
{
    public class FeatureDto
    {
       
        public string EducatorFullName { get; set; } = default!;

        public int Duration { get; set; }

      
        public double Rating { get; set; }
    }
}
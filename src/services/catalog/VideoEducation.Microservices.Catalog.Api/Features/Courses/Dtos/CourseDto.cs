using VideoEducation.Microservices.Catalog.Api.Features.Categories;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Dtos;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses.Dtos
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public Guid UserId  { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDto Category { get; set; } = default!;
        public FeatureDto Feature { get; set; } = default!;

    }
}

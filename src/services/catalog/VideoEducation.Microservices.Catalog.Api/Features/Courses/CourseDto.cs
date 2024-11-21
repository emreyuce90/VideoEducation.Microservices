namespace VideoEducation.Microservices.Catalog.Api.Features.Courses {
    public class CourseDto {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid CategoryId { get; set; }

    }
}

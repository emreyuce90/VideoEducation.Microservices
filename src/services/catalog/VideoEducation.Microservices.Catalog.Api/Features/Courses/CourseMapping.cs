using AutoMapper;
using VideoEducation.Microservices.Catalog.Api.Features.Courses.Create;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses {
    public class CourseMapping:Profile {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>().ReverseMap() ;
        }
    }
}

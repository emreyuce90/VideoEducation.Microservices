using AutoMapper;
using VideoEducation.Microservices.Catalog.Api.Features.Courses.Create;
using VideoEducation.Microservices.Catalog.Api.Features.Courses.Dtos;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses
{
    public class CourseMapping:Profile {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>().ReverseMap();
            CreateMap<CourseDto, Course>().ReverseMap();
            CreateMap<Feature,FeatureDto>().ReverseMap();

        }
    }
}

using VideoEducation.Microservices.Catalog.Api.Features.Categories.Create;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Delete;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.GetAll;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.GetById;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Update;
using VideoEducation.Microservices.Catalog.Api.Features.Courses.Create;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses {
    public static class CourseEndpointExt {
        public static void AddCourseGroupEndpointExt(this WebApplication application) {
            //Category ekleme apinin gruba dahil edilmesi
            application.MapGroup(prefix: "api/courses")
                .WithTags("Courses")
                .CreateCourseGroupItem();
               

        }
    }
}

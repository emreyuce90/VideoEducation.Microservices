using Asp.Versioning;
using Asp.Versioning.Builder;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Create;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Delete;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.GetAll;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.GetById;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Update;
using VideoEducation.Microservices.Catalog.Api.Features.Courses.Create;
using VideoEducation.Microservices.Catalog.Api.Features.Courses.Delete;
using VideoEducation.Microservices.Catalog.Api.Features.Courses.GetAll;
using VideoEducation.Microservices.Catalog.Api.Features.Courses.GetById;
using VideoEducation.Microservices.Catalog.Api.Features.Courses.GetByUserId;
using VideoEducation.Microservices.Catalog.Api.Features.Courses.Update;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses {
    public static class CourseEndpointExt {
        public static void AddCourseGroupEndpointExt(this WebApplication application, ApiVersionSet apiVersionSet) {
            //Category ekleme apinin gruba dahil edilmesi
            application.MapGroup(prefix: "api/v{version:apiVersion}/courses")
                .WithTags("Courses")
                .WithApiVersionSet(apiVersionSet)
                .CreateCourseGroupItem()
                .GetAllCourseGroupItem()
                .GetCourseByIdGroupItem()
                .UpdateCourseGroupItem()
                .DeleteCourseGroupItem()
                .GetCourseByUserIdGroupItem();



        }
    }
}

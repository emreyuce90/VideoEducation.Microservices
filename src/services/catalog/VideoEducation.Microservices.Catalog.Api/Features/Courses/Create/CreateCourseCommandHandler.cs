using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VideoEducation.Microservices.Catalog.Api.Repositories;
using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses.Create {
    public class CreateCourseCommandHandler(AppDbContext context,IMapper mapper) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>> {

        async Task<ServiceResult<Guid>> IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>.Handle(CreateCourseCommand request, CancellationToken cancellationToken) {

            var course = mapper.Map<Course>(request);

            var categoryExist = await context.Categories.AnyAsync(c=>c.Id == request.CategoryId,cancellationToken);
            
            if (!categoryExist) {
                return ServiceResult<Guid>.Error(title:"CategoryId not found",description:$"There is no such a categoryId {request.CategoryId} in our database",httpStatusCode:HttpStatusCode.NotFound);
            }

            var courseExist = await context.Courses.AnyAsync(course => course.Name == request.Name,cancellationToken);
            if (courseExist) {
                return ServiceResult<Guid>.Error(title: "This course already exist", description: $"This course named {request.Name} already saved in our database", httpStatusCode: HttpStatusCode.NotFound);

            }

            course.CreatedDate = DateTime.Now;
            course.Id = NewId.NextSequentialGuid();
            var feature = new Feature {
                Duration=100,
                EducatorFullName="Emre Yüce",
                Rating = 4.4
            };
            course.Feature = feature;

            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();
            return ServiceResult<Guid>.SuccessAsCreated(course.Id, $"api/courses/${course.Id}");
        }
    }
}

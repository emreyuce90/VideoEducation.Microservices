using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoEducation.Microservices.Catalog.Api.Features.Categories;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.GetAll;
using VideoEducation.Microservices.Catalog.Api.Features.Courses.Dtos;
using VideoEducation.Microservices.Catalog.Api.Repositories;
using VideoEducation.Microservices.Shared;
using VideoEducation.Microservices.Shared.Extensions;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses.GetAll {

    public record GetAllCourseQuery() : IRequestServiceResult<List<CourseDto>>;


    public class GetAllCourseHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCourseQuery, ServiceResult<List<CourseDto>>> {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCourseQuery request, CancellationToken cancellationToken) {

            var courses = await context.Courses.ToListAsync(cancellationToken);
            var allCategories = await context.Categories.ToListAsync(cancellationToken);

            foreach (var course in courses) {
                var category = new Category {
                    Id = course.CategoryId,
                    Name = allCategories.First(c => c.Id == course.CategoryId).Name, 
                };
                course.Category = category;
            }
            return ServiceResult<List<CourseDto>>.SuccessAsOk(mapper.Map<List<CourseDto>>(courses));
        }

    }

    public static class GetAllCourseEndpoint {
        public static RouteGroupBuilder GetAllCourseGroupItem(this RouteGroupBuilder group) {
            group.MapGet("/", async (IMediator mediator) => {
                var result = await mediator.Send(new GetAllCourseQuery());
                return result.ToGenericResult();
            }).MapToApiVersion(1,0);

            return group;
        }
    }

}

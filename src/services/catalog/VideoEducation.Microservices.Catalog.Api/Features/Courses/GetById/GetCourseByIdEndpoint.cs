using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.GetAll;
using VideoEducation.Microservices.Catalog.Api.Features.Courses.Dtos;
using VideoEducation.Microservices.Catalog.Api.Repositories;
using VideoEducation.Microservices.Shared;
using VideoEducation.Microservices.Shared.Extensions;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses.GetById
{


    public class GetCourseByIdHandler(AppDbContext context,IMapper mapper) : IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto>> {
        public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken) {
            var isCourseExist = await context.Courses.AnyAsync(course => course.Id == request.Id,cancellationToken);
            if (!isCourseExist) {
                return ServiceResult<CourseDto>.Error(title:"This course id cannot found on database",description:$"The course id you sent {request.Id} cannot found on our database",httpStatusCode:HttpStatusCode.NotFound);
            }

            var course = await context.Courses.FirstAsync(course => course.Id == request.Id,cancellationToken);
            var category = await context.Categories.FirstAsync(c=>c.Id ==course.CategoryId,cancellationToken);
            course.Category = category;

            return ServiceResult<CourseDto>.SuccessAsOk(mapper.Map<CourseDto>(course));
        }

    }


    public static class GetCourseByIdEndpoint {
        
            public static RouteGroupBuilder GetCourseByIdGroupItem(this RouteGroupBuilder group) {
                group.MapGet("/{id:guid}", async (Guid Id,IMediator mediator) => {
                    var result = await mediator.Send(new GetCourseByIdQuery(Id));
                    return result.ToGenericResult();
                }).MapToApiVersion(1,0);

                return group;
            }
        
    }
}

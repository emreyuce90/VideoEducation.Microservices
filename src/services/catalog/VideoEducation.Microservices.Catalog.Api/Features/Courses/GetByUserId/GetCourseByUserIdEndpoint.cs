using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VideoEducation.Microservices.Catalog.Api.Features.Courses.Dtos;
using VideoEducation.Microservices.Catalog.Api.Features.Courses.GetAll;
using VideoEducation.Microservices.Catalog.Api.Repositories;
using VideoEducation.Microservices.Shared;
using VideoEducation.Microservices.Shared.Extensions;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses.GetByUserId {

    public class GetCourseByUserIdHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCourseByUserIdQuery, ServiceResult<List<CourseDto>>> {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetCourseByUserIdQuery request, CancellationToken cancellationToken) {

            var course = await context.Courses.Where(c => c.UserId == request.UserId).ToListAsync();

            if (course is not null) {
                foreach (var c in course) {
                    var category = await context.Categories.FirstAsync(ca => ca.Id == c.CategoryId, cancellationToken);
                    c.Category = category;
                }
            }
            return ServiceResult<List<CourseDto>>.SuccessAsOk(mapper.Map<List<CourseDto>>(course));
        }
    }

    public static class GetCourseByUserIdEndpoint {
        public static RouteGroupBuilder GetCourseByUserIdGroupItem(this RouteGroupBuilder group) {
            group.MapGet("/user/{userId:guid}", async (IMediator mediator,Guid userId) => {
                var result = await mediator.Send(new GetCourseByUserIdQuery(userId));
                return result.ToGenericResult();
            }).MapToApiVersion(1,0);

            return group;
        }
    }
}

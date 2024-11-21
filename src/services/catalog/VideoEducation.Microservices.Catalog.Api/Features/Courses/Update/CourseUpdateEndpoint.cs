using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.GetAll;
using VideoEducation.Microservices.Catalog.Api.Repositories;
using VideoEducation.Microservices.Shared;
using VideoEducation.Microservices.Shared.Extensions;
using VideoEducation.Microservices.Shared.Filters;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses.Update {


    public class CourseUpdateCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CourseUpdateCommand, ServiceResult<CourseDto>> {
        public async Task<ServiceResult<CourseDto>> Handle(CourseUpdateCommand request, CancellationToken cancellationToken) {
            var courseExist = await context.Courses.AnyAsync(c => c.Id == request.Id, cancellationToken);
            if (!courseExist) {
                return ServiceResult<CourseDto>.Error("There is no course found with this id", $"This Id (${request.Id}) doesn't exist on our db ", HttpStatusCode.NotFound);
            }

            var categoryExist = await context.Categories.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);
            if (!categoryExist) {
                return ServiceResult<CourseDto>.Error("There is no category found with this id", $"This CategoryId (${request.CategoryId}) doesn't exist on our db ", HttpStatusCode.NotFound);

            }

            var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            course.Description = request.Description;
            course.Price = request.Price;
            course.Name = request.Name;
            course.PictureUrl = request.PicureUrl;
            course.CategoryId = request.CategoryId;

            context.Courses.Update(course);
            await context.SaveChangesAsync();
            return ServiceResult<CourseDto>.SuccessAsOk(mapper.Map<CourseDto>(course));
        }
    }


    public static class CourseUpdateEndpoint {

        public static RouteGroupBuilder UpdateCourseGroupItem(this RouteGroupBuilder group) {
            group.MapPut("/", async (CourseUpdateCommand request, IMediator mediator) => {
                var result = await mediator.Send(request);
                return result.ToGenericResult();
            }).AddEndpointFilter<ValidationFilter<CourseUpdateCommand>>();

            return group;
        }

    }
}

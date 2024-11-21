using AutoMapper;
using MediatR;
using MediatR.Registration;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.GetAll;
using VideoEducation.Microservices.Catalog.Api.Repositories;
using VideoEducation.Microservices.Shared;
using VideoEducation.Microservices.Shared.Extensions;

namespace VideoEducation.Microservices.Catalog.Api.Features.Courses.Delete {


    public class DeleteCategoryHandler(AppDbContext context,IMapper mapper) : IRequestHandler<DeleteCategoryCommand, ServiceResult<Guid>> {
        public async  Task<ServiceResult<Guid>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) {
            var courseExist = await context.Courses.AnyAsync(c => c.Id == request.Id, cancellationToken);
            if (!courseExist) {
                return ServiceResult<Guid>.Error("This course was not found on our database",$"The courseId (${request.Id}) cannot found in our database",HttpStatusCode.NotFound);
            }

            var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            context.Courses.Remove(course);
            await context.SaveChangesAsync();
            return ServiceResult<Guid>.SuccessAsOk(course.Id);
        }
    }


    
        public static class GetAllCategoryEndpoint {
            public static RouteGroupBuilder DeleteCourseGroupItem(this RouteGroupBuilder group) {
                group.MapDelete("/{Id:guid}", async (Guid Id,IMediator mediator) => {
                    var result = await mediator.Send(new DeleteCategoryCommand(Id));
                    return result.ToGenericResult();
                });

                return group;
            }
        }
    
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.GetAll;
using VideoEducation.Microservices.Catalog.Api.Repositories;
using VideoEducation.Microservices.Shared;
using VideoEducation.Microservices.Shared.Extensions;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.Delete {
    public record DeleteCategoryCommand(Guid Id) : IRequestServiceResult<DeleteCategoryCommandResponse>;
    public record DeleteCategoryCommandResponse(Guid Id);
    /// <summary>
    /// Requestten gelen Id parametresini alır , ilgili kategori kaydını siler ve sildiği kaydın id sini geriye döndürür
    /// </summary>
    /// <param name="context"></param>
    public class DeleteCategoryHandler (AppDbContext context): IRequestHandler<DeleteCategoryCommand, ServiceResult<DeleteCategoryCommandResponse>> {
        async Task<ServiceResult<DeleteCategoryCommandResponse>> IRequestHandler<DeleteCategoryCommand, ServiceResult<DeleteCategoryCommandResponse>>.Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) {

          

            var category = await context.Categories.FirstOrDefaultAsync(c=> c.Id == request.Id,cancellationToken);

            if (category == null) {
                return ServiceResult<DeleteCategoryCommandResponse>.Error(title:"The Id parameter cannot be null", description:$"The id you requested {request.Id} is not exist on our db ", httpStatusCode: HttpStatusCode.NotFound);

            }

            context.Categories.Remove(category);
            context.SaveChanges();
            return ServiceResult<DeleteCategoryCommandResponse>.SuccessAsOk(new DeleteCategoryCommandResponse(request.Id));
        }
    }

    public static class DeleteCategoryEndpoint {
        public static RouteGroupBuilder DeleteCategoryGroupItem(this RouteGroupBuilder group) {
            group.MapDelete("/{id:guid}", async (Guid Id,IMediator mediator) => {
                var result = await mediator.Send(new DeleteCategoryCommand(Id));
               return result.ToGenericResult();
            });

            return group;
        }
    }
}

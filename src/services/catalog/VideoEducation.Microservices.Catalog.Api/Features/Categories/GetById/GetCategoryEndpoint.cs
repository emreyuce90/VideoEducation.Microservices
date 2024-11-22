using MediatR;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Dtos;
using VideoEducation.Microservices.Shared.Extensions;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.GetById {
    public static class GetCategoryEndpoint {

       
            public static RouteGroupBuilder GetCategoryGroupItem(this RouteGroupBuilder group) {
                group.MapGet("/{id:guid}", async (Guid Id,IMediator mediator) => {
                    var result = await mediator.Send(new GetCategoryQuery(Id));
                    return result.ToGenericResult();
                }).MapToApiVersion(1.0);

                return group;
            }
        }
    
}

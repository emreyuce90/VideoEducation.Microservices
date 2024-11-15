using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.Create {
    public static class CreateCategoryEndpoint {

        public static  RouteGroupBuilder CreateCategoryGroupItem(this RouteGroupBuilder group) {
            group.MapPost("/", async (CreateCategoryCommand command,IMediator mediator) => {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            });

            return group; 
        }
    }
}

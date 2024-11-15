using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.Create {
    public static class CreateCategoryEndpoint {

        public static  RouteGroupBuilder CreateCategoryGroupItem(this RouteGroupBuilder group) {
            group.MapPost("/", async (CreateCategoryCommand command,IMediator mediator) => {
                var result = await mediator.Send(command);
                return new ObjectResult(result) {
                    StatusCode = 200
                };
            });

            return group; 
        }
    }
}

using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using VideoEducation.Microservices.Shared.Extensions;
using VideoEducation.Microservices.Shared.Filters;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpoint {

        public static  RouteGroupBuilder CreateCategoryGroupItem(this RouteGroupBuilder group) {
            group.MapPost("/", async (CreateCategoryCommand command,IMediator mediator) => {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            }).AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();

            return group; 
        }
    }
}

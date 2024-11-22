using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Create;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Dtos;
using VideoEducation.Microservices.Catalog.Api.Repositories;
using VideoEducation.Microservices.Shared;
using VideoEducation.Microservices.Shared.Extensions;
using VideoEducation.Microservices.Shared.Filters;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.GetAll {

    //Endpointimizde herhangi bir data beklemiyoruz
    //Geri dönüş değeri olarak CaegoryDto listesi döneceğiz
    public record GetAllCategoryQuery() : IRequestServiceResult<List<CategoryDto>>;

    public class GetAllCategoryHandler(AppDbContext context,IMapper mapper) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>> {
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken) {

            var categories = await context.Categories.ToListAsync(cancellationToken);
           
            return ServiceResult<List<CategoryDto>>.SuccessAsOk(mapper.Map<List<CategoryDto>>(categories));
        }

    }

    public static class GetAllCategoryEndpoint {
        public static RouteGroupBuilder GetAllCategoryGroupItem(this RouteGroupBuilder group) {
            group.MapGet("/", async (IMediator mediator) => {
                var result = await mediator.Send(new GetAllCategoryQuery());
                return result.ToGenericResult();
            }).MapToApiVersion(1,0);

            return group;
        }
    }
}

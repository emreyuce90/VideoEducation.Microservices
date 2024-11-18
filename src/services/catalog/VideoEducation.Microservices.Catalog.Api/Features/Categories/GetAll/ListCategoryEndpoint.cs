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
    public record GetAllCategoryQuery() : IRequest<ServiceResult<List<CategoryDto>>>;

    public class GetAllCategoryHandler(AppDbContext context) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>> {
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken) {

            var categories = await context.Categories.ToListAsync();
            var categoryListDto = new List<CategoryDto>();
            
            foreach (var c in categories) {
                var dto = new CategoryDto { Id = c.Id, Name = c.Name };
                categoryListDto.Add(dto);
            }

            return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoryListDto);
        }

    }

    public static class GetAllCategoryEndpoint {
        public static RouteGroupBuilder GetAllCategoryGroupItem(this RouteGroupBuilder group) {
            group.MapGet("/", async (IMediator mediator) => {
                var result = await mediator.Send(new GetAllCategoryQuery());
                return result.ToGenericResult();
            });

            return group;
        }
    }
}

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Dtos;
using VideoEducation.Microservices.Catalog.Api.Repositories;
using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.GetById {
    public class GetCategoryHandler (AppDbContext context,IMapper mapper): IRequestHandler<GetCategoryQuery, ServiceResult<CategoryDto>> {
        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken) {
            var categorydb = await context.Categories.FirstOrDefaultAsync(c=> c.Id == request.Id,cancellationToken);
            
            //Fast fail
            if (categorydb is null) {
            return ServiceResult<CategoryDto>.Error("The category you requested is not exist on db",System.Net.HttpStatusCode.NotFound);
            }

            return ServiceResult<CategoryDto>.SuccessAsOk(mapper.Map<CategoryDto>(categorydb));

        }
    }
}

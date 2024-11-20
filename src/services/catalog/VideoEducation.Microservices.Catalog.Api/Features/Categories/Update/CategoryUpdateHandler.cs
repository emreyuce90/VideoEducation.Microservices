using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Dtos;
using VideoEducation.Microservices.Catalog.Api.Repositories;
using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.Update {
    public class CategoryUpdateHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CategoryUpdateCommandRequest, ServiceResult<CategoryUpdateDto>> {
   
        public async Task<ServiceResult<CategoryUpdateDto>> Handle(CategoryUpdateCommandRequest request, CancellationToken cancellationToken) {
            var categoryExist = await context.Categories.AnyAsync(x => x.Id == request.CategoryUpdateDto.Id, cancellationToken);

            if (!categoryExist) {
                return ServiceResult<CategoryUpdateDto>.Error(title: "CategoryId doesn't exist on database", description: $"CategoryId {request.CategoryUpdateDto.Id} not found on database", HttpStatusCode.NotFound);
            }

            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryUpdateDto.Id, cancellationToken);

            category.Name = request.CategoryUpdateDto.Name;

             context.Categories.Update(category);
            await context.SaveChangesAsync();
            return ServiceResult<CategoryUpdateDto>.SuccessAsOk(mapper.Map<CategoryUpdateDto>(category));
        }
    }
}

using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Repositories;
using VideoEducation.Microservices.Catalog.Api.Repositories;
using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.Create {
    public class CreateCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>> {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken) {

            //veritabanında eşleşen kaydı getir
            var categoryExist = await categoryRepository.IsCategoryExist(request.Name,cancellationToken);
            
            if (categoryExist) {
                return ServiceResult<CreateCategoryResponse>.Error(title: "The category already exist", description: $"The category name {request.Name} already exist in db", HttpStatusCode.BadRequest);
            }

            var category = new Category {
                Id = NewId.NextSequentialGuid(),
                Name = request.Name,
            };

            try {
                await categoryRepository.AddCategory(category, cancellationToken);
                return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id), url: "");
            } catch (Exception ex) {
                return ServiceResult<CreateCategoryResponse>.Error(title:"An error occured while creatin category",description: ex.Message,HttpStatusCode.BadRequest);
                throw;
            }
        }
    }
}

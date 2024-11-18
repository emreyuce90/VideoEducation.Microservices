using AutoMapper;
using VideoEducation.Microservices.Catalog.Api.Features.Categories.Dtos;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories {
    public class CategoryMapper :Profile{
        public CategoryMapper()
        {
            CreateMap<Category, CategoryDto>().ReverseMap(); 
        }
    }
}

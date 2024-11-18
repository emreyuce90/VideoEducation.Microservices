using MediatR;
using VideoEducation.Microservices.Shared;

namespace VideoEducation.Microservices.Catalog.Api.Features.Categories.Create {
    // Genelde bu tarz işler için recordlar kullanılrır
    // recordlar immutable dır nesne örneği alındıktan sonra atama yapılır tekrar atama yapılmaz
    // 
    //public record CreateCategoryCommand {
    //    public CreateCategoryCommand(string name) {
    //        Name = name;
    //    }
    //    public string Name { get; init; }

    //}
    //Kısa yazım şekli
    public record CreateCategoryCommand(string Name): IRequestServiceResult<CreateCategoryResponse>;
}

using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEducation.Microservices.Shared.Filters {
    public class ValidationFilter<T> : IEndpointFilter {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next) {

            //dependency injection üzerinden validator talep edilir
            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
            if(validator is null) {
               return await next(context);
            }

            //Validate edeceğimiz tipte model HTTP endpointte mevcut mu ona bakıyoruz eğer get isteği ise burası null dır
            var requestModel = context.Arguments.OfType<T>().FirstOrDefault();
            if (requestModel is null) {
                return await next(context);
            }

            var validationResult = await validator.ValidateAsync(requestModel);
            if (!validationResult.IsValid) {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }
            
           return await next(context);
        }
    }
}

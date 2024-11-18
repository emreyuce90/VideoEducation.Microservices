using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEducation.Microservices.Shared.Extensions {
    public static class CommonServiceExt {
        public static IServiceCollection AddCommonServiceExt(this IServiceCollection services,Type assembly) {
            services.AddHttpContextAccessor();
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));
            //FluentValidation Aspnet Core paketi yüklendi ve aşağıdaki konfigürasyonlar yapıldı
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(assembly);

            services.AddAutoMapper(assembly);
            return services;
        }
    }
}

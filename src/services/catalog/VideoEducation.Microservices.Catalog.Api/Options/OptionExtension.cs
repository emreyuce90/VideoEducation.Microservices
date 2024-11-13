using Microsoft.Extensions.Options;

namespace VideoEducation.Microservices.Catalog.Api.Options {
    public static class OptionExtension {
        //Geriye servis döndürme sebebimiz program.cs tarafında üzerine yeni servisler ekleyebilmemiz içindir
        public static IServiceCollection AddOptionsExt(this IServiceCollection services) {
            //MongoOption classını appsettings.jsondaki json objemize bind ettik
            services.AddOptions<MongoOption>().BindConfiguration(nameof(MongoOption)).ValidateDataAnnotations().ValidateOnStart();

            //MongoOption'ı dependency injectiona ekler
            //Singleton olarak eklendiği için bir kez oluşturulur
            // IOptions<T> arayüzü appsettings.jsondaki değerleri okur ve MongoOptiona mapler
            // Value dediğimizde gerçek ayarlara ulaşabiliriz

            services.AddSingleton(sp=>sp.GetRequiredService<IOptions<MongoOption>>().Value);

            return services;
        }
    }
}

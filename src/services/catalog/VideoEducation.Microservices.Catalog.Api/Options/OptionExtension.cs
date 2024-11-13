namespace VideoEducation.Microservices.Catalog.Api.Options {
    public static class OptionExtension {
        //Geriye servis döndürme sebebimiz program.cs tarafında üzerine yeni servisler ekleyebilmemiz içindir
        public static IServiceCollection AddOptionsExt(this IServiceCollection services) {
            //MongoOption classını appsettings.jsondaki json objemize bind ettik
            services.AddOptions<MongoOption>().BindConfiguration(nameof(MongoOption)).ValidateDataAnnotations().ValidateOnStart();

            return services;
        }
    }
}

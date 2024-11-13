using MongoDB.Driver;
using VideoEducation.Microservices.Catalog.Api.Repositories;

namespace VideoEducation.Microservices.Catalog.Api.Options
{
    public static class RepositoryExt
    {
        public static IServiceCollection AddDatabaseServiceExt(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                //Yukarıda oluşturmuş olduğum MongoOption'a di dan eriştim cs i aldım
                var options = sp.GetRequiredService<MongoOption>();
                return new MongoClient(options.ConnectionString);
            });

            services.AddScoped(sp =>
            {
                // Yukarıda doldurduğum için artık mongo clienta sahibim
                var mongoClient = sp.GetRequiredService<IMongoClient>();
                var options = sp.GetRequiredService<MongoOption>();
                return AppDbContext.Create(mongoClient.GetDatabase(options.DatabaseName));
            });
            return services;
        }
    }
}

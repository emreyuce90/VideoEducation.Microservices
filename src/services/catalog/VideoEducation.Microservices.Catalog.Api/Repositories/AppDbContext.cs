using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection;
using MongoDB.Driver;
using VideoEducation.Microservices.Catalog.Api.Features.Categories;
using VideoEducation.Microservices.Catalog.Api.Features.Courses;

namespace VideoEducation.Microservices.Catalog.Api.Repositories {
    //AppDbContext içerisine DbContextOptions tipinde bir parametre alır
    //DbContext sınıfından inherit edilir ve aldığı bu parametreyi de DbContext içerisine geçer
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {


        public static AppDbContext Create(IMongoDatabase database) {
            var contextOption = new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);
            return new AppDbContext(contextOption.Options);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}

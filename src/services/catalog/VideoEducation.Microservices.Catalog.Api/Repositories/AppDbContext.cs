using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection;
using VideoEducation.Microservices.Catalog.Api.Features.Categories;
using VideoEducation.Microservices.Catalog.Api.Features.Courses;

namespace VideoEducation.Microservices.Catalog.Api.Repositories {
    //AppDbContext içerisine DbContextOptions tipinde bir parametre alır
    //DbContext sınıfından inherit edilir ve aldığı bu parametreyi de DbContext içerisine geçer
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}

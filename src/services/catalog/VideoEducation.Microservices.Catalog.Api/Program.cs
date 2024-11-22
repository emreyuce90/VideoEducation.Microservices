using MongoDB.Driver;
using VideoEducation.Microservices.Catalog.Api.Features.Categories;
using VideoEducation.Microservices.Catalog.Api.Features.Courses;
using VideoEducation.Microservices.Catalog.Api.Options;
using VideoEducation.Microservices.Catalog.Api.Repositories;
using VideoEducation.Microservices.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(Program));
builder.Services.AddVersioningExt();
var app = builder.Build();

app.AddCategoryGroupEndpointExt(app.AddVersionSetExt());
app.AddCourseGroupEndpointExt(app.AddVersionSetExt());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();



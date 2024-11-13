using VideoEducation.Microservices.Catalog.Api.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//appsettings teki mongo options alanını tip güvenli kullanmamızı sağlar
builder.Services.AddOptionsExt();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();



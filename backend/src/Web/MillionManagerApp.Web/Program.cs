using Microsoft.OpenApi.Models;
using MillionManagerApp.Application.Services;
using MillionManagerApp.Domain.Interfaces;
using MillionManagerApp.Infrastructure.Repositories;
using MillionManagerApp.Web.Middleware;
using MongoDB.Driver;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// ======================
//   Logs
// ======================
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();


// ======================
//   MongoDB
// ======================
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb");
var mongoDatabaseName = builder.Configuration["MongoDbDatabaseName"];
var frontendOrigin = builder.Configuration.GetValue<string>("FrontendSettings:AllowedCorsOrigin");

builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(mongoConnectionString));

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(mongoDatabaseName);
});

// ======================
//   CORS
// ======================
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyCorsPolicy",
                      builder =>
                      {
                          builder.WithOrigins(frontendOrigin)
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
});

// ======================
//   REPOS / SERVICES
// ======================
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
builder.Services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();
builder.Services.AddScoped<PropertyService>();

// ======================
//   CONTROLLERS & SWAGGER
// ======================
builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
});

// ======================
//   Swagger config
// ======================
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MillionManagerApp API",
        Version = "v1",
        Description = "API de prueba técnica Fullstack..."
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MillionManagerApp API v1");
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors("MyCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();




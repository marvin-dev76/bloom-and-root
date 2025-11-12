using BloomAndRoot.API.Middleware;
using BloomAndRoot.Application.Features.Plants.Commands.AddPlant;
using BloomAndRoot.Application.Features.Plants.Commands.UpdatePlant;
using BloomAndRoot.Application.Features.Plants.Queries.GetAllPlants;
using BloomAndRoot.Application.Features.Plants.Queries.GetPlantById;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Infrastructure.Data;
using BloomAndRoot.Infrastructure.Repositories;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>((options) =>
{
  var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
  var serverVersion = ServerVersion.AutoDetect(connectionString);
  options.UseMySql(connectionString, serverVersion);
});
builder.Services.AddScoped<IPlantRepository, PlantRepository>();
builder.Services.AddScoped<GetAllPlantsQueryHandler>();
builder.Services.AddScoped<GetPlantByIdQueryHandler>();
builder.Services.AddScoped<AddPlantCommandHandler>();
builder.Services.AddScoped<UpdatePlantCommandHandler>();
builder.Services.AddControllers().AddJsonOptions((options) =>
{
  options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.Strict;
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
